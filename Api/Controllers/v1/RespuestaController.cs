namespace Api.Controllers.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Package.Utilities.Net;
    /// <summary>
    /// Api Para Exponer los Metodos de la Entidad [Respuesta]
    /// </summary>
    public partial class RespuestaController : BaseControllerDm<Application.Features.Models.Dto.RespuestaDto, Domain.AggregateModels.Respuesta>
    {
        /// <summary>
        /// Obtener un registro por [{id}] de la Entidad [Proyecto] con las respuestas
        /// </summary>
        /// <param name="idProyecto">id Del Registro</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Registro encontrado por por [{id}] de la Entidad (Proyecto)
        ///         404NotFound Sí no se encuentra un registro por [{id}] de la Entidad (Proyecto)
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad [Proyecto]
        /// </remarks>
        /// <response code="200">Retorna Un Registro encontrado por [{id}] de la Entidad (Proyecto)</response>
        /// <response code="404">Sí no se encuentra un registro por [{id}] de la Entidad (Proyecto)</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpGet("GetQuestionsWithAnwers/{idProyecto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Application.Features.Models.Dto.ProyectoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetQuestionsWithAnwers(Guid idProyecto)
        {
            var result = await _mediator.Send(new Application.Models.Respuesta.GetAnwersByProject(idProyecto));

            return StatusCode(StatusCodes.Status200OK, result);
        }


        /// <summary>
        /// Retorna Todos los Registros de la Entidad
        /// </summary>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Lista de Registros de la Entidad [Respuesta]
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad [Respuesta]
        /// </remarks>
        /// <response code="200">Retorna la Lista de Registros de la Entidad [Respuesta]</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Application.Features.Models.Dto.RespuestaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAsync() { return await ToListAsync(); }

        /// <summary>
        /// Retorna Todos los Registros Paginados, Ordenados y en la dirección de ordenamiento Configurada
        /// </summary>
        /// <param name="parameterGetList">Parametrizar las configuraciones para la lista, Paginación, Ordenamiento</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Lista de Registros de la Entidad [Respuesta] y Un objeto con los datos de la paginación
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad [Respuesta]
        /// </remarks>
        /// <response code="200">Retorna Un Objeto Compuesto, Lista de Registros de la Entidad [Respuesta] y Un objeto con los datos de la paginación</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpGet("GetPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Application.Features.Models.Dto.RespuestaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRecordsOrderPagedFilter([FromQuery] ParameterGetList parameterGetList) { return await GetListOrderPaged(parameterGetList); }

        /// <summary>
        /// Retorna Todos los Registros Paginados, Ordenados, dirección de ordenamiento Configurada y filtrados según los parametros configurada
        /// </summary>
        /// <param name="parameterGetList">Parametrizar las configuraciones para la lista, Paginación, Ordenamiento</param>
        /// <param name="objectFilter">Objeto con los valores de los filtros</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Lista de Registros de la Entidad [Respuesta] y Un objeto con los datos de la paginación
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad [Respuesta]
        /// </remarks>
        /// <response code="200">Retorna Un Objeto Compuesto, Lista de Registros de la Entidad [Respuesta] y Un objeto con los datos de la paginación</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpPost("GetPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Application.Features.Models.Dto.RespuestaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRecordsOrderPagedFilter([FromQuery] ParameterGetList parameterGetList, [FromBody] Filter objectFilter)
        {
            return await GetListOrderPaged(parameterGetList, objectFilter);
        }

        /// <summary>
        /// Obtener un registro por [{id}] de la Entidad [Respuesta]
        /// </summary>
        /// <param name="id">id Del Registro</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Registro encontrado por por [{id}] de la Entidad (Respuesta)
        ///         404NotFound Sí no se encuentra un registro por [{id}] de la Entidad (Respuesta)
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad [Respuesta]
        /// </remarks>
        /// <response code="200">Retorna Un Registro encontrado por [{id}] de la Entidad (Respuesta)</response>
        /// <response code="404">Sí no se encuentra un registro por [{id}] de la Entidad (Respuesta)</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Application.Features.Models.Dto.RespuestaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<IActionResult> Get(string id)
        {
            return GetGenericByIdAsync(id.ToString());
        }


        /// <summary>
        /// recibe una lista de Respuestas para eliminar
        /// </summary>
        /// <param name="listObjBase">Lista de objetos que seran eliminados</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Respuesta de la operacion y el Objeto de ResponseApi
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Retorna Respuesta de la operacion y el Objeto de ResponseApi</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpPost("DeleteList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteListPostAsync([FromBody] IList<Application.Features.Models.Dto.RespuestaDto> listObjBase)
        {
            var validate = await _mediator.Send(new Application.Features.Commands.DeleteListEntitiesAsyncCommand<Application.Features.Models.Dto.RespuestaDto, Domain.AggregateModels.Respuesta>(listObjBase, nameof(Application.Features.Models.Dto.RespuestaDto.Preguntaid)));
            if (validate?.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, validate);
            }
            return ResultApiAsync(listObjBase[0]);
        }


        /// <summary>
        /// Eliminar un registro por [{id}] de la Entidad Respuesta
        /// </summary>
        /// <param name="id">id Del Registro de Respuesta</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Respuesta de la operacion y el Objeto de ResponseApi
        ///         404NotFound Sí no se encuentra un registro por [{id}] para eliminar de la Entidad  Respuesta
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
        public async override Task<IActionResult> Delete(string id)
        {
            var objBase = await _mediator.Send(new Application.Features.Queries.GetEntityAsyncById<Application.Features.Models.Dto.RespuestaDto, Domain.AggregateModels.Respuesta>(id));
            return await DeleteListPostAsync(new List<Application.Features.Models.Dto.RespuestaDto>() { objBase ?? new Application.Features.Models.Dto.RespuestaDto() { Id = "-1", Preguntaid = string.Empty } });
        }
    }

}

