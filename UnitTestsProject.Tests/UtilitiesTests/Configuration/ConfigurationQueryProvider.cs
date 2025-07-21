using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace UnitTestsProject.Tests
{
    public static class QueryRepositoryTestExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class, new()
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>() { CallBase = true };
            dbSet.As<IDbAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(queryable.GetEnumerator()));

            dbSet.As<IAsyncEnumerable<T>>()
                        .Setup(m => m.GetAsyncEnumerator(default))
                        .Returns(new TestAsyncEnumerator<T>(queryable.GetEnumerator()));

            dbSet.As<IQueryable<T>>()
                 .Setup(m => m.Provider)
                 .Returns(new TestAsyncQueryProvider<T>(queryable.Provider));

            dbSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(queryable.Provider));

            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<T>(queryable.Provider));
            dbSet.As<IQueryable<T>>()
               .Setup(m => m.Provider)
               .Returns(new TestAsyncQueryProvider<T>(queryable.Provider));
            dbSet.As<System.Data.Entity.IDbSet<T>>().Setup(m => m.Create()).Returns(new T());
            dbSet.As<System.Data.Entity.IDbSet<T>>().Setup(m => m.Add(It.IsAny<T>())).Returns<T>(i => { sourceList.Add(i); return i; });
            dbSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<T>>()));
            dbSet.As<System.Data.Entity.IDbSet<T>>().Setup(m => m.Remove(It.IsAny<T>())).Returns<T>(i => { sourceList.Remove(i); return i; });
            dbSet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<T>>()));
            dbSet.Setup(x => x.Add(It.IsAny<T>())).Returns<T>(i => { sourceList.Add(i); return null; });
            dbSet.Setup(x => x.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
               .Callback((T artist, CancellationToken token) => { })
               .ReturnsAsync(It.IsAny<EntityEntry<T>>());
            dbSet.Setup(x => x.Remove(It.IsAny<T>())).Returns<T>(i => { sourceList.Remove(i); return null; });
            dbSet.Setup(x => x.Update(It.IsAny<T>())).Returns<T>(i => { sourceList.Add(i); return null; });
            return dbSet.Object;
        }
    }

    internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestDbAsyncQueryProvider(IQueryProvider inner) { _inner = inner; }
        public IQueryable CreateQuery(Expression expression) { return new TestDbAsyncEnumerable<TEntity>(expression); }
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) { return new TestDbAsyncEnumerable<TElement>(expression); }
        public object Execute(Expression expression) { return _inner.Execute(expression); }
        public TResult Execute<TResult>(Expression expression) { return _inner.Execute<TResult>(expression); }
        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken) { return Task.FromResult(Execute(expression)); }
        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) { return Task.FromResult(Execute<TResult>(expression)); }
    }

    internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>
    {
        public TestDbAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
        public TestDbAsyncEnumerable(Expression expression) : base(expression) { }
        public IDbAsyncEnumerator<T> GetAsyncEnumerator() { return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator()); }
        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() { return GetAsyncEnumerator(); }
        public IQueryProvider Provider { get { return new TestDbAsyncQueryProvider<T>(this); } }
    }

    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {

        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner) { _inner = inner; }
        public void Dispose() { _inner.Dispose(); }
        public Task<bool> MoveNextAsync(CancellationToken cancellationToken) { return Task.FromResult(_inner.MoveNext()); }
        public T Current { get { return _inner.Current; } }
        object IDbAsyncEnumerator.Current { get { return Current; } }
    }


    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public static IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new TestAsyncEnumerable<TResult>(expression);
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }



        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            var expectedResultType = typeof(TResult).GetGenericArguments()[0];
            var executionResult = typeof(IQueryProvider)
                                 .GetMethod(
                                      name: nameof(IQueryProvider.Execute),
                                      genericParameterCount: 1,
                                      types: new[] { typeof(Expression) })
                                 .MakeGenericMethod(expectedResultType)
                                 .Invoke(this, new[] { expression });

            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                                        ?.MakeGenericMethod(expectedResultType)
                                         .Invoke(null, new[] { executionResult });
        }
    }

    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            var obj = new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            return obj;
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public T Current
        {
            get
            {
                return _inner.Current;
            }
        }

        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return await Task.FromResult(_inner.MoveNext());
        }

        public async ValueTask DisposeAsync()
        {
            _inner.Dispose();
            await Task.Yield();
        }
    }
}
