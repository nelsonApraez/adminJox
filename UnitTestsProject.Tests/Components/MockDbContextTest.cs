using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.AggregateModels.Moneda.Specs;
using Domain.AggregateModels.ValueObjects;
using Domain.Common;
using Infrastructure.Common;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestsProject.Tests.Test.MockData.Entities;

namespace UnitTestsProject.Tests.Components
{

    public class MockDbContextTest<ENT> : BaseTestBasic where ENT : class, new()
    {
        protected readonly IRepositoryBase<ENT> _repositoryBase;
        private readonly List<ENT> _lsbase;

        public MockDbContextTest(List<ENT> lsbase, IRepositoryBase<ENT> repositoryBase)
        {
            _lsbase = lsbase;
            _repositoryBase = repositoryBase;
            repositoryBase.SetAutoSave(true);
            _repositoryBase.CreateAsync(lsbase).GetAwaiter().GetResult();
        }

        public override void BuildMockData()
        {
            _repositoryBase?.SetAutoSave(true);
        }

        #region Base Test Repository        

        [TestMethod]
        public async Task Given_ModelValidate_When_AddEntityIntoRepository_Then_IsAddModelInMemoryDatabase()
        {
            _repositoryBase.RepositoryContext.Set<ENT>().ToArrayAsync().GetAwaiter().GetResult().ToList().ForEach(item => _repositoryBase.RepositoryContext.Entry<ENT>(item).State = EntityState.Detached);
            await _repositoryBase.EditAsync(_lsbase);
            var objReturnSh = await _repositoryBase.SearchAsync(RepositoryTestExtensions.GetExpressionEqual<ENT>(1));
            Assert.IsNotNull(objReturnSh);
            var objReturnShIn = await _repositoryBase.SearchListAsync(RepositoryTestExtensions.GetExpressionEqual<ENT>(1), null);
            Assert.IsNotNull(objReturnShIn);
            var objReturnCn = _repositoryBase.Count(RepositoryTestExtensions.GetExpressionEqual<ENT>(1));
            Assert.IsTrue(objReturnCn > 0);
            var objReturnEnt = _repositoryBase.Search(RepositoryTestExtensions.GetExpressionEqual<ENT>(1));
            Assert.IsNotNull(objReturnEnt);
            _repositoryBase.RepositoryContext.Set<ENT>().ToArrayAsync().GetAwaiter().GetResult().ToList().ForEach(item => _repositoryBase.RepositoryContext.Entry<ENT>(item).State = EntityState.Detached);
            await _repositoryBase.DeleteRangeAsync(_lsbase);
            var objReturn = await _repositoryBase.CreateAsync(_lsbase[1]);
            Assert.IsTrue(objReturn != null && objReturn.Value > 0);
        }

        [TestMethod]
        public async Task Given_NotFilter_When_FetchDataEntity_Then_AreReturnedAllDataObjectsModel()
        {
            //Act
            var lstReturn = await _repositoryBase.ToListAsync();

            ////Assert
            Assert.IsTrue(lstReturn != null && lstReturn.Count > 0);
        }

        [TestMethod]
        public async Task Given_NotFilter_When_FetchDataEntity_Then_AreReturnedAllDataObjectsModelPageAndOrderBy()
        {
            //Act
            var lstReturn = await _repositoryBase.ToListPaged();

            ////Assert
            Assert.IsTrue(lstReturn != null && lstReturn.List.Any());
        }

        #endregion

    }
    public static class InMemoryDbContextFactory
    {
        public static MainContext GetDbContext(IMediator mediator)
        {
            var options = new DbContextOptionsBuilder<MainContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var dbContext = new MainContext(options, mediator);
            return dbContext;
        }
    }

    [TestClass]
    public class RepositoryModelTest : MockDbContextTest<Domain.AggregateModels.Moneda.Moneda>
    {
        public RepositoryModelTest() : base(MonedaMockData.GetList(), new MonedaRepository(InMemoryDbContextFactory.GetDbContext(null)))
        {

        }

        [TestMethod]
        public async Task Given_NotFilter_When_FetchDataEntity_Then_AreReturnedAllDataObjectsModelPage()
        {
            //Act
            var lstReturn = await _repositoryBase.ToListPaged();

            ////Assert
            Assert.IsTrue(lstReturn != null);
        }

        [TestMethod]
        public void Given_NotValue_When_ValidateValueObject_Then_AreReturnedDomainException()
        {
            //Act
            var strnom = NombreValido.CreateEmpty(nameof(Domain.AggregateModels.Moneda.Moneda.Nombre), MonedaMetadata.Nombre).Value.Valor;
            var strDates = EstadoRangoFechas.CalcularEstado(DateTime.Now.AddDays(1), DateTime.Now).Value;
            var strnomEqual = NombreValido.CreateEqual(string.IsNullOrEmpty(strnom) ? nameof(Domain.AggregateModels.Moneda.Moneda.Nombre) : strnom, MonedaMetadata.Nombre);
            var lstReturn = NombreValido.Create(strnomEqual.ToString() + strDates.ToString(), MonedaMetadata.Nombre).Value;

            ////Assert
            Assert.IsTrue(lstReturn != null);
        }
    }
}
