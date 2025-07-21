namespace Api.Controllers.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Package.Utilities.Net;
    using Application.Features.Models.Dto;

    /// <summary>
    /// Api Para Exponer los Metodos de la Entidad [Processingresult]
    /// </summary>
    public partial class ProcessingresultController : BaseControllerDm<Application.Features.Models.Dto.ProcessingresultDto, Domain.AggregateModels.Processingresult>
    {
        /// <summary>
        /// Obtener un registro por [{id}] de la Entidad [Proyecto] con las sugerencias, beneficios y estrategias
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
        [HttpGet("GetByProject/{idProyecto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Application.Features.Models.Dto.ProyectoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByProject(Guid idProyecto)
        {
            var result = await _mediator.Send(new Application.Models.Processingresult.GetProcessingresultByProject(idProyecto));

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Procesar las preguntas y respuestas
        /// </summary>
        /// <param name="idProyecto">id Del Registro</param>
        /// <returns>
        /// El resultado del procesamiento, con las sugerencias.
        /// 
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
        [HttpPost("ProcessQuestionsAndAnswers/{idProyecto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Application.Features.Models.Dto.ProyectoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ProcessQuestionsAndAnswers(Guid idProyecto)
        {  
            var result = await _mediator.Send(new Application.Models.Processingresult.ProcessQuestionsAndAnswers(idProyecto));

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Retorna Todos los Registros de la Entidad
        /// </summary>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Lista de Registros de la Entidad [Processingresult]
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad [Processingresult]
        /// </remarks>
        /// <response code="200">Retorna la Lista de Registros de la Entidad [Processingresult]</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Application.Features.Models.Dto.ProcessingresultDto>))]
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
        ///         200OK Lista de Registros de la Entidad [Processingresult] y Un objeto con los datos de la paginación
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad [Processingresult]
        /// </remarks>
        /// <response code="200">Retorna Un Objeto Compuesto, Lista de Registros de la Entidad [Processingresult] y Un objeto con los datos de la paginación</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpGet("GetRecordsOrderPagedFilter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Application.Features.Models.Dto.ProcessingresultDto>))]
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
        ///         200OK Lista de Registros de la Entidad [Processingresult] y Un objeto con los datos de la paginación
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad [Processingresult]
        /// </remarks>
        /// <response code="200">Retorna Un Objeto Compuesto, Lista de Registros de la Entidad [Processingresult] y Un objeto con los datos de la paginación</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpPost("GetRecordsOrderPagedFilter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Application.Features.Models.Dto.ProcessingresultDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRecordsOrderPagedFilter([FromQuery] ParameterGetList parameterGetList, [FromBody]Filter objectFilter) {
            return await GetListOrderPaged(parameterGetList, objectFilter);
        }

        /// <summary>
        /// Obtener un registro por [{id}] de la Entidad [Processingresult]
        /// </summary>
        /// <param name="id">id Del Registro</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Registro encontrado por por [{id}] de la Entidad (Processingresult)
        ///         404NotFound Sí no se encuentra un registro por [{id}] de la Entidad (Processingresult)
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad [Processingresult]
        /// </remarks>
        /// <response code="200">Retorna Un Registro encontrado por [{id}] de la Entidad (Processingresult)</response>
        /// <response code="404">Sí no se encuentra un registro por [{id}] de la Entidad (Processingresult)</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Application.Features.Models.Dto.ProcessingresultDto))]
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
        /// recibe una lista de Processingresults para eliminar
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
        public async Task<IActionResult> DeleteListPostAsync([FromBody] IList<Application.Features.Models.Dto.ProcessingresultDto> listObjBase)
        {
            var validate = await _mediator.Send(new Application.Features.Commands.DeleteListEntitiesAsyncCommand<Application.Features.Models.Dto.ProcessingresultDto, Domain.AggregateModels.Processingresult> (listObjBase, nameof(Application.Features.Models.Dto.ProcessingresultDto.Proyectoid )));
            if (validate?.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, validate);
            }
            return ResultApiAsync(listObjBase[0]);
        }
        

		   /// <summary>
        /// Eliminar un registro por [{id}] de la Entidad Processingresult
        /// </summary>
        /// <param name="id">id Del Registro de Processingresult</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Respuesta de la operacion y el Objeto de ResponseApi
        ///         404NotFound Sí no se encuentra un registro por [{id}] para eliminar de la Entidad  Processingresult
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
        public async override  Task<IActionResult> Delete(string id)
        {
            var objBase = await _mediator.Send(new Application.Features.Queries.GetEntityAsyncById<Application.Features.Models.Dto.ProcessingresultDto , Domain.AggregateModels.Processingresult>(id) );
            return await DeleteListPostAsync(new List<Application.Features.Models.Dto.ProcessingresultDto>() {  objBase ?? new Application.Features.Models.Dto.ProcessingresultDto() { Id ="-1",Proyectoid = string.Empty}}  );
        }

        //http get metod by name Download by idproyect an return a file pdf
        [HttpGet("Download/{idProyecto}")]
        public async Task<IActionResult> Download(Guid idProyecto)
        {
            var result = await _mediator.Send(new Application.Models.Processingresult.GetProcessingFileByProject(idProyecto));
            string fileName = "Processingresult" + DateTime.Now + ".pdf";
            return File(result, "application/pdf", fileName);
        }


    }

}

