namespace UnitTestsProject.Tests

{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.BaseApplicationHelper;
    using Application.Features.Commands;
    using Application.Features.Queries;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Package.Utilities.Net;


    /// <summary>
    /// Esta Clase representa la base para los Test y se encarga de generar los mock de los objetos
    /// </summary>
    /// <typeparam name="DTO">Clase de implementacion genicas para patron DTO</typeparam>
    /// <typeparam name="ENT">Clase generica que representa entidad de dominio</typeparam>
    public abstract class BaseTestDm<DTO, ENT, Service> : BaseTest<DTO, ENT, Service>
            where DTO : class, new()
            where ENT : class, new()
            where Service : BaseApplicationHelper<ENT>
    {
        protected BaseTestDm() : base()
        {
        }


        #region Base Test Commnands Mediatr

        [TestMethod]
        public virtual async Task Given_ModelIsValidate_When_CreateEntity_Then_IsConfirmedTransaction()
        {
            adaptaterBase.AdaptaterMock.Setup(item => item.SaveChangesAsync(default)).Returns(Task.FromResult(1));
            //Act
            var objReturn = await _mediator.Send(new CreateEntityAsyncCommand<DTO, ENT>(objDtoInitExits));

            adaptaterBase.AdaptaterMock.Verify(m => m.Set<ENT>().AddAsync(It.IsAny<ENT>(), It.IsAny<CancellationToken>()), Times.Once());
            adaptaterBase.AdaptaterMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            ////Assert
            Assert.IsTrue(objReturn != null && objReturn.Value > 0);
        }

        [TestMethod]
        public virtual async Task Given_ModelIsValidate_When_UpdateEntity_Then_IsConfirmedTransaction()
        {
            adaptaterBase.AdaptaterMock.Setup(item => item.SaveChangesAsync(default)).Returns(Task.FromResult(1));
            //Act
            var objReturn = await _mediator.Send(new EditEntityAsyncCommand<DTO, ENT>(objDtoInitExits));
            adaptaterBase.AdaptaterMock.Verify(m => m.Set<ENT>().Update(It.IsAny<ENT>()), Times.Once());
            adaptaterBase.AdaptaterMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            ////Assert
            Assert.IsNotNull(objReturn);
        }

        [TestMethod]
        public virtual async Task Given_FilterIsByKey_When_FetchDataEntity_Then_IsReturnedModel()
        {
            adaptaterBase.AdaptaterMock.Setup(item => item.SaveChangesAsync(default)).Returns(Task.FromResult(1));
            //Act
            var objReturn = await _mediator.Send(new GetEntityAsyncById<DTO, ENT>(PkValue));

            ////Assert
            Assert.IsNotNull(objReturn);
        }

        [TestMethod]
        public virtual async Task Given_FilterIsEmpty_When_FetchDataEntity_Then_AreReturnedAllData()
        {
            adaptaterBase.AdaptaterMock.Setup(item => item.SaveChangesAsync(default)).Returns(Task.FromResult(1));
            //Act
            var lstReturn = await _mediator.Send(new GetDataEntityAsync<DTO, ENT>(new Filter()));

            ////Assert
            Assert.IsTrue(lstReturn != null && lstReturn.Any());
        }

        [TestMethod]
        public virtual async Task Given_NotFilter_When_FetchDataEntity_Then_AreReturnedAllData()
        {
            adaptaterBase.AdaptaterMock.Setup(item => item.SaveChangesAsync(default)).Returns(Task.FromResult(1));
            //Act
            var lstReturn = await _mediator.Send(new ToListEntityAsync<DTO, ENT>());

            ////Assert
            Assert.IsTrue(lstReturn != null && lstReturn.Any());
        }

        [TestMethod]
        public virtual async Task Given_ModelIsValidate_When_DeleteEntities_Then_IsConfirmedTransaction()
        {
            adaptaterBase.AdaptaterMock.Setup(item => item.SaveChangesAsync(default)).Returns(Task.FromResult(1));
            //Act
            var objReturn = await _mediator.Send(new DeleteListEntitiesAsyncCommand<DTO, ENT>(new List<DTO>() { objDtoInitExits }, RepositoryTestExtensions.GetKeyName<ENT>()));
            adaptaterBase.AdaptaterMock.Verify(m => m.Set<ENT>().Remove(It.IsAny<ENT>()));
            adaptaterBase.AdaptaterMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()));
            ////Assert
            Assert.IsNotNull(objReturn);
        }


        [TestMethod]
        public virtual async Task Given_ModelIsComprobate_When_ValidateEntity_Then_PassedInvariance()
        {
            adaptaterBase.AdaptaterMock.Setup(item => item.SaveChangesAsync(default)).Returns(Task.FromResult(1));
            //Act
            var objReturn = await _mediator.Send(new ValidateRulesEntityAsync<DTO, ENT>(objDtoInitExits, EnumerationApplication.Operations.Validate));

            ////Assert
            Assert.IsNotNull(objReturn);
        }

        [TestMethod]
        public virtual async Task Given_ModelIsComprobate_When_ValidateDuplicateEntity_Then_PassedInvariance()
        {
            adaptaterBase.AdaptaterMock.Setup(item => item.SaveChangesAsync(default)).Returns(Task.FromResult(1));
            //Act
            var objReturn = await _mediator.Send(new ValidateDuplicatedEntityAsync<DTO, ENT>(objDtoInitExits));

            ////Assert
            Assert.IsNotNull(objReturn);
        }


        [TestMethod]
        public virtual async Task Given_ModelIsNotComprobate_When_ValidateDuplicateEntity_Then_UnapprovedInvariance()
        {
            adaptaterBase.AdaptaterMock.Setup(item => item.SaveChangesAsync(default)).Returns(Task.FromResult(1));
            //Act
            var objReturn = await _mediator.Send(new ValidateDuplicatedEntityAsync<DTO, ENT>(objDtoInitExitNotExist));

            ////Assert
            Assert.IsNotNull(objReturn);
        }

        [TestMethod]
        public virtual async Task Given_ModelIsValidate_When_DeleteEntity_Then_IsConfirmedTransaction()
        {
            adaptaterBase.AdaptaterMock.Setup(item => item.SaveChangesAsync(default)).Returns(Task.FromResult(1));
            var objReturn = await _mediator.Send(new DeleteEntityAsyncCommand<DTO, ENT>(objDtoInitExits));
            adaptaterBase.AdaptaterMock.Verify(m => m.Set<ENT>().Remove(It.IsAny<ENT>()), Times.Once());
            adaptaterBase.AdaptaterMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            ////Assert
            Assert.IsTrue(objReturn != null);
        }


        [TestMethod]
        public virtual async Task Given_FilterIsApply_When_FetchDataEntity_Then_IsPagedOrderByDynamic()
        {
            //Act
            var lstReturn = await _mediator.Send(new ToListEntityPagedAsync<DTO, ENT>(
                new ParameterGetList() { DirecOrder = "Asc", NumberRecords = 2, OrderBy = RepositoryTestExtensions.GetKeyName<ENT>(), Page = 1 },
                new Filter()
                {
                    Filters = new List<ItemsFilters>()
                     {
                         new() {
                             Name = RepositoryTestExtensions.GetKeyName<ENT>(),
                             Values = new[] {PkValue },
                             Operator = EnumerationApplication.OperationExpression.Like
                         }
                     }
                }));

            ////Assert
            Assert.IsTrue(lstReturn != null && lstReturn.List.Any());
        }
        #endregion
    }
}
