namespace Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Features.Commands;
    using Application.Features.Queries;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Package.Utilities.Net;
    using Package.Utilities.Net.Excel;

    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public abstract partial class BaseControllerDm<DTO, ENT> : BaseUtilities
        where DTO : class, new()
        where ENT : class, new()
    {

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

        #region Metodos Tranversales de la APIs


        /// <summary>
        /// Crear Registro Nuevo de la entidad
        /// </summary>
        /// <param name="objBase">Objeto A Crear</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Respuesta de la operacion y el Objeto de ResponseApi
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad <Typeparamref name="DTO"/>
        /// </remarks>
        /// <response code="200">Retorna Respuesta de la operacion y el Objeto de ResponseApi</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> PostAsync([FromBody] DTO objBase)
        {
            ValidateAuthorizationPermissions(EnumerationApplication.Operations.Create);
            var validate = await _mediator.Send(new ValidateRulesEntityAsync<DTO, ENT>(objBase, EnumerationApplication.Operations.Create));
            if (validate?.Count > 0)
            {
                return ResultApiValidationRules(validate);
            }
            var objParam = GetParameters(objBase);
            return ResultApi(((await _mediator.Send(new CreateEntityAsyncCommand<DTO, ENT>(objBase)) ?? 0) > 0), CusMessageCreate, objParam, GetParametersObj(objBase));
        }


        /// <summary>
        /// Validar reglas de metadatos de la entidad
        /// </summary>
        /// <param name="objBase">Objeto A Validar</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Respuesta de la operacion y el Objeto de ResponseApi
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad <Typeparamref name="DTO"/>
        /// </remarks>
        /// <response code="200">Retorna Respuesta de la operacion y el Objeto de ResponseApi</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpPost("Validator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> ValidatorPostAsync([FromBody] DTO objBase)
        {
            var validate = await _mediator.Send(new ValidateRulesEntityAsync<DTO, ENT>(objBase, EnumerationApplication.Operations.Validate));
            if (validate?.Count > 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validate);
            }
            return ResultApiAsync(GetParameters(objBase));
        }

        /// <summary>
        /// Actualizar Registro de la entidad
        /// </summary>
        /// <param name="objBase">Objeto a Modificar</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Respuesta de la operacion y el Objeto de ResponseApi
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad <Typeparamref name="DTO"/>
        /// </remarks>
        /// <response code="200">Retorna Respuesta de la operacion y el Objeto de ResponseApi</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> PutAsync([FromBody] DTO objBase)
        {

            ValidateAuthorizationPermissions(EnumerationApplication.Operations.Update);
            var validate = await _mediator.Send(new ValidateRulesEntityAsync<DTO, ENT>(objBase, EnumerationApplication.Operations.Update));
            if (validate?.Count > 0)
            {
                return ResultApiValidationRules(validate);
            }
            var objParam = GetParameters(objBase);

            return ResultApi((await _mediator.Send(new EditEntityAsyncCommand<DTO, ENT>(objBase))) ?? false, CusMessageUpdate, objParam, GetParametersObj(objBase));
        }

        /// <summary>
        /// Eliminar un registro por [{id}] de la Entidad
        /// </summary>
        /// <param name="id">id Del Registro</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Respuesta de la operacion y el Objeto de ResponseApi
        ///         404NotFound Sí no se encuentra un registro por [{id}] para eliminar de la Entidad 
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad <ENTypeparamref name="DTO"/>
        /// </remarks>
        /// <response code="200">Retorna Respuesta de la operacion y el Objeto de ResponseApi</response>
        /// <response code="404">Sí no se encuentra un registro por [{id}] para eliminar de la Entidad </response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> Delete(string id)
        {
            return await DeleteGenericAsync(id, id);
        }


        /// <summary>
        /// Count all Entities 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        public virtual async Task<long> GetCount()
        {
            object[] values = { "0" };
            ItemsFilters item = new ItemsFilters()
            {
                Name = "ID",
                Operator = EnumerationApplication.OperationExpression.NotEquals,
                Values = values
            };
            List<ItemsFilters> itemsFilters = new List<ItemsFilters>() { item };
            Filter objFilter = new Filter() { Filters = itemsFilters };
            var liscount = await _mediator.Send(new ToListEntityPagedAsync<DTO, ENT>(new() { Page = 1, NumberRecords = 1 }, objFilter));
            return liscount.Paged.MaxCount;
        }

        #endregion

        #region Metodos Genericos para las APIs Buscar/Listar/Eliminar

        /// <summary>
        /// Eliminar Registro de tipo <ENTypeparamref name="DTO"/>
        /// </summary>
        /// <param name="id">identificador del elemento que se busca</param>
        /// <param name="ids">Codigos Revisividos para eliminación</param>
        /// <returns>
        /// Retorna en el response de la petición
        ///              Status200OK Con la respuesta de la operacion y el Objeto de ResponseApi
        ///              Status404NotFound Si no se encuentra un regsitro por Id(s) de la Entidad
        ///              Status400BadRequest Sí ocurrió una falla, validación o error controlado
        ///              Status500InternalServerError Sí ocurrió una falla o error NO controlado
        ///              Status403Forbidden Sí no tiene permisos para ejecutar la acción
        /// </returns>
        protected async Task<IActionResult> DeleteGenericAsync(string id, params string[] ids)
        {
            ValidateAuthorizationPermissions(EnumerationApplication.Operations.Remove);
            var objBase = await _mediator.Send(new GetEntityAsyncById<DTO, ENT>(id));
            if (objBase.IsNotNull())
            {
                var validate = await _mediator.Send(new ValidateRulesEntityAsync<DTO, ENT>(objBase, EnumerationApplication.Operations.Remove));
                if (validate?.Count > 0)
                {
                    return ResultApiValidationRules(validate);
                }
                GetParameters(objBase);
                return ResultApi((await _mediator.Send(new DeleteEntityAsyncCommand<DTO, ENT>(objBase))) ?? false, CusMessageDelete, ids, GetParametersObj(objBase));
            }

            return ResultApi(StatusCodes.Status404NotFound, EnumerationException.Message.ErrNoEncontrado, ids);
        }

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
        protected async Task<IActionResult> GetListOrderPaged(ParameterGetList parameterGetList)
        {
            return await GetListOrderPaged(parameterGetList, new Filter());
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
        protected async Task<IActionResult> GetListOrderPaged(ParameterGetList parameterGetList, Filter objFilter)
        {
            ValidateAuthorizationPermissions(EnumerationApplication.Operations.Read);
            return ResultApi(await _mediator.Send(new ToListEntityPagedAsync<DTO, ENT>(parameterGetList, objFilter)));
        }

        /// <summary>
        /// Retorna Todos los Registros
        /// </summary>AutoMapperMappingException: Missing type map configuration or unsupported mapping.

        /// <returns>UnauthorizedAccessException: 
        /// Retorna en el response de la petición
        ///              Status200OK Con la Lista de Registros de la Entidad y Un objeto con los datos de la paginación
        ///              Status400BadRequest Sí ocurrió una falla, validación o error controlado
        ///              Status500InternalServerError Sí ocurrió una falla o error NO controlado
        ///              Status403Forbidden Sí no tiene permisos para ejecutar la acción
        /// </returns>
        protected async Task<IActionResult> ToListAsync()
        {
            ValidateAuthorizationPermissions(EnumerationApplication.Operations.Read);
            return ResultApi(await _mediator.Send(new ToListEntityAsync<DTO, ENT>()));
        }

        /// <summary>
        /// Retorna el Registro Por Id Entontrado de tipo <ENTypeparamref name="DTO"/>
        /// </summary>
        /// <param name="id">identificador del elemento que se busca</param>
        /// <param name="ids">Codigos Revisividos para busqueda</param>
        /// <returns>
        /// Retorna en el response de la petición
        ///              Status200OK Con el registro encontrado por Id(s) de la Entidad
        ///              Status404NotFound Si no se encuentra un regsitro por Id(s) de la Entidad
        ///              Status400BadRequest Sí ocurrió una falla, validación o error controlado
        ///              Status500InternalServerError Sí ocurrió una falla o error NO controlado
        ///              Status403Forbidden Sí no tiene permisos para ejecutar la acción
        /// </returns>
        protected async Task<IActionResult> GetGenericByIdAsync(string id, params string[] ids)
        {
            ValidateAuthorizationPermissions(EnumerationApplication.Operations.Read);
            var objBase = await _mediator.Send(new GetEntityAsyncById<DTO, ENT>(id));
            if (objBase.IsNotNull())
            {
                return ResultApi(objBase);
            }

            return ResultApi(StatusCodes.Status404NotFound, EnumerationException.Message.ErrNoEncontrado, ids);
        }


        /// <summary>
        /// Comprobar duplicidad de registro
        /// </summary>
        /// <param name="objBase">Objeto A Crear</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Respuesta de la operacion y el Objeto de ResponseApi
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad <Typeparamref name="DTO"/>
        /// </remarks>
        /// <response code="200">Retorna Respuesta de la operacion y el Objeto de ResponseApi</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpPost("RecordExist")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> RecordExistPostAsync([FromBody] DTO objBase)
        {
            ValidateAuthorizationPermissions(EnumerationApplication.Operations.Read);
            var validate = await _mediator.Send(new ValidateDuplicatedEntityAsync<DTO, ENT>(objBase));
            if (validate?.Count > 0)
            {
                return BadRequest(validate);
            }
            return ResultApiAsync(objBase);
        }
        #endregion
    }
}
