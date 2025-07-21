namespace Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Features.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Package.Utilities.Net;
    using Package.Utilities.Net.Excel;

    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public abstract partial class BaseControllerDmList<DTO, ENT> : BaseUtilities
        where DTO : class, new()
        where ENT : class, new()
    {
        /// <summary>
        /// Constructor para inicializar la base con el ServiceApplication de la entidad
        /// </summary>
        /// <param name="mediator"></param>
        protected BaseControllerDmList(IMediator mediator) : base(mediator)
        {
            NameClassReference = typeof(ENT).Name;
        }

        #region Exportar a Excel

        /// <summary>
        /// Exportar información solicitada de la Entidad a excel
        /// </summary>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Archivo con la información solicitada de la Entidad <ENTypeparamref name="DTO"/>
        ///         204NoContent Validaciones en la generacion del Excel
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad <Typeparamref name="DTO"/>
        /// </remarks>
        /// <response code="200">Retorna Un Archivo con la información solicitada de la Entidad 
        /// <response code="204">Retorna Validaciones en la generacion del Excel</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpPost("ExportExcelData")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(byte[]))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual Task<IActionResult> ExportExcelData([FromBody] Filter objFilter)
        {
            return ExportExcelData(async () => await _mediator.Send(new GetDataEntityAsync<DTO, ENT>(objFilter, TimeZone)));
        }

        /// <summary>
        /// Generación de Archivo de excel con la información Solicitada
        /// </summary>
        /// <typeparam name="TE">Tipo de la Lista a Exportar</typeparam>
        /// <param name="fnList">Function Encargada de Consultar la Información
        /// <returns>Excel Generado con la información</returns>
        protected async Task<IActionResult> ExportExcelData<TE>(Func<Task<List<TE>>> fnList)
            where TE : class, new()
        {
            ValidateAuthorizationPermissions(EnumerationApplication.Operations.Read);

            FileExcel fileExcel = await Excel.GetFileExcelAsync(fnList, NameClassReference);

            return File(fileExcel.FileStream, "application/octet-stream", fileExcel.FileName);
        }

        #endregion


        /// <summary>
        /// Retorna Todos los Registros Paginados segun el 
        ///     numero de registros, Ordenados y en la dirección Configurada de tipo <ENTypeparamref name="DTO"/>
        /// </summary>
        /// <param name="parameterGetList">Parametrizar las configuraciones para la lista, Paginación, Ordenamiento</param>
        /// <returns>
        /// Retorna en el response de la petición
        ///              Status200OK Con la Lista de Registros de la Entidad y Un objeto con los datos de la paginación
        ///              Status400BadRequest Sí ocurrió una falla, validación o error controlado
        ///              Status500InternalServerError Sí ocurrió una falla o error NO controlado
        ///              Status403Forbidden Sí no tiene permisos para ejecutar la acción
        /// </returns>
        protected async Task<IActionResult> GetListEntityOrderPaged(ParameterGetList parameterGetList)
        {
            return await GetListEntityOrderPaged(parameterGetList, new Filter());
        }

        /// <summary>
        /// Retorna Todos los Registros Paginados segun el 
        ///     numero de registros, Ordenados y en la dirección Configurada de tipo <ENTypeparamref name="DTO"/>
        /// </summary>
        /// <param name="parameterGetList">Parametrizar las configuraciones para la lista, Paginación, Ordenamiento</param>
        /// <param name="objFilter">Objeto con los valores filtro</param>
        /// <returns>
        /// Retorna en el response de la petición
        ///              Status200OK Con la Lista de Registros de la Entidad y Un objeto con los datos de la paginación
        ///              Status400BadRequest Sí ocurrió una falla, validación o error controlado
        ///              Status500InternalServerError Sí ocurrió una falla o error NO controlado
        ///              Status403Forbidden Sí no tiene permisos para ejecutar la acción
        /// </returns>
        protected async Task<IActionResult> GetListEntityOrderPaged(ParameterGetList parameterGetList, Filter objFilter)
        {
            return ResultApi(await _mediator.Send(new ToListEntityPagedAsync<DTO, ENT>(parameterGetList, objFilter)));
        }

        /// <summary>
        /// Retorna el Registro Por Id Entontrado de tipo <ENTypeparamref name="DTO"/>
        /// </summary>
        /// <param name="codigo">identificador del elemento que se busca</param>        
        /// <returns>
        /// Retorna en el response de la petición
        ///              Status200OK Con el registro encontrado por Id(s) de la Entidad
        ///              Status404NotFound Si no se encuentra un regsitro por Id(s) de la Entidad
        ///              Status400BadRequest Sí ocurrió una falla, validación o error controlado
        ///              Status500InternalServerError Sí ocurrió una falla o error NO controlado
        ///              Status403Forbidden Sí no tiene permisos para ejecutar la acción
        /// </returns>
        protected async Task<IActionResult> GetEntityByIdAsync(int codigo)
        {
            var objBase = await _mediator.Send(new GetEntityAsyncById<DTO, ENT>(codigo.ToString()));
            if (objBase.IsNotNull())
            {
                return ResultApi(objBase);
            }

            return ResultApi(StatusCodes.Status404NotFound, EnumerationException.Message.ErrNoEncontrado, new string[] { codigo.ToString() });
        }
    }
}
