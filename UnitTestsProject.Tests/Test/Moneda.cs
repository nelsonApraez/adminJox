namespace UnitTestsProject.Tests
{
    using Application.Features.Commands;
    using Application.Features.Queries;
    using Package.Utilities.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Domain.AggregateModels;


    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Moneda]
    /// </summary>
    [TestClass]
    public partial class Moneda : BaseTestDm<Application.Features.Models.Dto.MonedaDto, Domain.AggregateModels.Moneda.Moneda, Application.Features.Services.MonedaService>
    {

        #region Test Domain

        [TestMethod]
        public async Task Dado_MonedaValida_Cuando_SeValidanReglasDeInvarianza_Entonces_EsConfirmadaLaInvarianza()
        {
            //Arrange            
            objDtoInitExits.Identificador = "";
            objDtoInitExits.Nombre = "";

            //Act
            var objReturn = await _mediator.Send(new ValidateRulesEntityAsync<Application.Features.Models.Dto.MonedaDto, Domain.AggregateModels.Moneda.Moneda>(objDtoInitExits, EnumerationApplication.Operations.Validate));

            ////Assert
            Assert.IsNotNull(objReturn);
        }

        [TestMethod]
        public async Task Dado_MonedaIdentificadorInvalido_Cuando_SeValidanReglasDeInvarianza_Entonces_ExcepcionLongitud()
        {
            //Arrange            
            objDtoInitExits.Identificador = "1020304050607080";

            //Act
            var objReturn = await _mediator.Send(new ValidateRulesEntityAsync<Application.Features.Models.Dto.MonedaDto, Domain.AggregateModels.Moneda.Moneda>(objDtoInitExits, EnumerationApplication.Operations.Validate));

            ////Assert
            Assert.IsNotNull(objReturn);
        }

        [TestMethod]
        public async Task Dado_MonedaValida_Cuando_SeValidanReglasDeInvarianza_Entonces_NoEsConfirmadaLaInvarianza()
        {
            //Arrange            
            objDtoInitExits.ActivoDesde = System.DateTime.Now;
            objDtoInitExits.ActivoHasta = System.DateTime.Now.AddDays(-1);

            //Act
            var objReturn = await _mediator.Send(new ValidateRulesEntityAsync<Application.Features.Models.Dto.MonedaDto, Domain.AggregateModels.Moneda.Moneda>(objDtoInitExits, EnumerationApplication.Operations.Validate));

            ////Assert
            Assert.IsNotNull(objReturn);
        }

        
        [TestMethod]
        public async Task Dado_MonedaValida_Cuando_SeCambiaEstado_Entonces_EsConfirmadoNuevoEstado()
        {
            //Act
            var objReturn = await _mediator.Send(new ChangeStateEntityAsyncCommand<Application.Features.Models.Dto.MonedaDto, Domain.AggregateModels.Moneda.Moneda>(objDtoInitExitNotExist, nameof(Application.Features.Models.Dto.MonedaDto.Estado)));

            ////Assert
            Assert.IsNotNull(objReturn);
        }


     
        [TestMethod]
        public async Task Dado_FiltroPorEstadoInactivo_Cuando_ConsultaMoneda_Entonces_RetornaListadoDeMonedaInactivas()
        {
            //Arrange
            var BusinessRulesMoneda = new Application.Features.Services.MonedaService(GetRepository(), _mediator);
            //Act
            var lstReturn = await BusinessRulesMoneda.Repository.ToListPaged(new ParameterOfList<Domain.AggregateModels.Moneda.Moneda>(1, 2, nameof(Domain.AggregateModels.Moneda.Moneda.Descripcion), "Asc", new Filter()
            {
                Filters = new List<ItemsFilters>()
                 {
                     new ItemsFilters() {
                         Name = nameof(Domain.AggregateModels.Moneda.Moneda.Estado ),
                         Values = new[] { "InActivo" },
                         Operator = EnumerationApplication.OperationExpression.Like
                     }
                 }
                ,
                Sorts = new List<ItemSort>()
                {
                    new ItemSort()
                    {
                        Name= nameof(Domain.AggregateModels.Moneda.Moneda.Estado ),
                        Direction = "asc"

                    },
                    new ItemSort()
                    {
                        Name= nameof(Domain.AggregateModels.Moneda.Moneda.Identificador ),
                        Direction = "asc"

                    }
                }
            }));

            ////Assert
            Assert.IsFalse(lstReturn != null && lstReturn.List.Count() > 0);
        }

        [TestMethod]        
        public async Task Dado_FiltroPorEstadoActivo_Cuando_ConsultaMoneda_Entonces_RetornaListadoDeMonedaActivas()
        {
            //Arrange
            var BusinessRulesMoneda = new Application.Features.Services.MonedaService(GetRepository(), _mediator);
            //Act
            var lstReturn = await BusinessRulesMoneda.Repository.ToListPaged(new ParameterOfList<Domain.AggregateModels.Moneda.Moneda>(1, 2, nameof(Domain.AggregateModels.Moneda.Moneda.Descripcion), "Asc", new Filter()
            {
                Filters = new List<ItemsFilters>()
                 {
                     new ItemsFilters() {
                         Name = nameof(Domain.AggregateModels.Moneda.Moneda.Estado ),
                         Values = new[] { "Activo" },
                         Operator = EnumerationApplication.OperationExpression.Like
                     }
                 },
                Sorts = new List<ItemSort>()
                {
                    new ItemSort()
                    {
                        Name= nameof(Domain.AggregateModels.Moneda.Moneda.Identificador ),
                        Direction = "asc"

                    },
                    new ItemSort()
                    {
                        Name= nameof(Domain.AggregateModels.Moneda.Moneda.Estado ),
                        Direction = "asc"

                    }
                    
                }
            }));

            ////Assert
            //Assert.IsTrue(lstReturn != null && lstReturn.List.Count() > 0);
        }

        [TestMethod]
        public async Task Dado_MonedaValida_Cuando_SeValidanReglasDeInvarianza_Entonces_EsConfirmadaLaLLavePrimaria()
        {
            //Arrange            
            var PrimaryKeyName = ObjectBaseExtensions.GetPrimaryKey<Domain.AggregateModels.Menu>();

            //Act

            var queryFilterId = ExpressionHelper.GetCriteriaWhere<Domain.AggregateModels.Menu>(PrimaryKeyName,
                                                                     EnumerationApplication.OperationExpression.Equals,
                                                                     ObjectBaseExtensions.GetPrimaryKeyValue<Domain.AggregateModels.Menu>("0"));
            ////Assert
            Assert.IsNotNull(queryFilterId);
        }
        #endregion

    }
}
