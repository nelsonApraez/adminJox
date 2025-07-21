namespace Api.Controllers
{
    using Application.Features.Models.Dto;
    using Application.Features.Queries;
    using Domain.AggregateModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Package.Utilities.Net;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// API to expose the methods for the entity [Menu]
    /// </summary>
    public partial class MenuController : BaseControllerDm<Application.Features.Models.Dto.MenuDto, Domain.AggregateModels.Menu>
    {
        /// <summary>
        /// Returns All Entity Records
        /// </summary>
        /// <returns>
        /// Returns an HTTP response with StatusCodes
        ///         200OK List of Entity Records [Menu]
        ///         400BadRequest If a failure, validation or controlled error occurred
        ///         500InternalServerError If a failure or error occurred that was NOT controlled
        ///         403Forbidden If you do not have permissions to execute the action
        ///         401Unauthorized If you are not authenticated
        /// </returns>
        /// <remarks>
        /// Entity [Menu]
        /// </remarks>
        /// <response code="200">Returns a List of Entity Records [Menu]</response>
        /// <response code="400">If a failure, validation or controlled error occurred</response>
        /// <response code="500">If a failure or error occurred that was NOT controlled</response>
        /// <response code="403">If you do not have permissions to execute the action</response>
        /// <response code="401">If you are not authenticated</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Application.Features.Models.Dto.MenuDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAsync() { return await ToListAsync(); }

        /// <summary>
        /// Returns All Records Paginated, Sorted and in the Configured sorting direction
        /// </summary>
        /// <param name="parameterGetList">Parameterize settings for list, Pagination, Sorting</param>
        /// <returns>
        /// Returns an HTTP response with StatusCodes
        ///         200OK List of Entity Records [Menu] and an object with the pagination data
        ///         400BadRequest If a failure, validation or controlled error occurred
        ///         500InternalServerError If a failure or error occurred that was NOT controlled
        ///         403Forbidden If you do not have permissions to execute the action
        ///         401Unauthorized If you are not authenticated
        /// </returns>
        /// <remarks>
        /// Entity [Menu]
        /// </remarks>
        /// <response code="200">Returns a composite object, List of Entity Records [Menu] and an object with the pagination data</response>
        /// <response code="400">If a failure, validation or controlled error occurred</response>
        /// <response code="500">If a failure or error occurred that was NOT controlled</response>
        /// <response code="403">If you do not have permissions to execute the action</response>
        /// <response code="401">If you are not authenticated</response>
        [HttpGet("GetPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Application.Features.Models.Dto.MenuDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRecordsOrderPagedFilter([FromQuery] ParameterGetList parameterGetList) { return await GetListOrderPaged(parameterGetList); }

        /// <summary>
        /// Returns All Records Paginated, Sorted, Configured sorting address and filtered according to the configured parameters
        /// </summary>
        /// <param name="parameterGetList">Parameterize settings for list, Pagination, Sorting</param>
        /// <param name="objectFilter">Object with filter values</param>
        /// <returns>
        /// Returns an HTTP response with StatusCodes
        ///         200OK List of Entity Records [Menu] and an object with the pagination data
        ///         400BadRequest If a failure, validation or controlled error occurred
        ///         500InternalServerError If a failure or error occurred that was NOT controlled
        ///         403Forbidden If you do not have permissions to execute the action
        ///         401Unauthorized If you are not authenticated
        /// </returns>
        /// <remarks>
        /// Entity [Menu]
        /// </remarks>
        /// <response code="200">Returns a composite object, List of Entity Records [Menu] and an object with the pagination data</response>
        /// <response code="400">If a failure, validation or controlled error occurred</response>
        /// <response code="500">If a failure or error occurred that was NOT controlled</response>
        /// <response code="403">If you do not have permissions to execute the action</response>
        /// <response code="401">If you are not authenticated</response>
        [HttpPost("GetPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Application.Features.Models.Dto.MenuDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRecordsOrderPagedFilter([FromQuery] ParameterGetList parameterGetList, [FromBody]Filter objectFilter) {
            return await GetListOrderPaged(parameterGetList, objectFilter);
        }

        /// <summary>
        /// Get a record by [{id}] from Entity [Menu]]
        /// </summary>
        /// <param name="id">Record id</param>
        /// <returns>
        /// Returns an HTTP response with StatusCodes
        ///         200OK Record found by by [{id}] of the Entity [Menu])
        ///         404NotFound If a record is not found for [{id}] of the Entity [Menu]
        ///         400BadRequest If a failure, validation or controlled error occurred
        ///         500InternalServerError If a failure or error occurred that was NOT controlled
        ///         403Forbidden If you do not have permissions to execute the action
        ///         401Unauthorized If you are not authenticated
        /// </returns>
        /// <remarks>
        /// Entity [Menu]
        /// </remarks>
        /// <response code="200">Returns A Record found by [{id}] of Entity [Menu]</response>
        /// <response code="404">If a record is not found for [{id}] of the Entity [Menu]</response>
        /// <response code="400">If a failure, validation or controlled error occurred</response>
        /// <response code="500">If a failure or error occurred that was NOT controlled</response>
        /// <response code="403">If you do not have permissions to execute the action</response>
        /// <response code="401">If you are not authenticated</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Application.Features.Models.Dto.MenuDto))]
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
        /// Receives a list of Menu to delete
        /// </summary>
        /// <param name="listObjBase">List of objects that will be deleted</param>
        /// <returns>
        /// Returns an HTTP response with StatusCodes
        ///         200OK Operation response and ResponseApi Object
        ///         400BadRequest If a failure, validation or controlled error occurred
        ///         500InternalServerError If a failure or error occurred that was NOT controlled
        ///         403Forbidden If you do not have permissions to execute the action
        ///         401Unauthorized If you are not authenticated
        /// </returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Operation response and ResponseApi Object</response>
        /// <response code="400">If a failure, validation or controlled error occurred</response>
        /// <response code="500">If a failure or error occurred that was NOT controlled</response>
        /// <response code="403">If you do not have permissions to execute the action</response>
        /// <response code="401">If you are not authenticated</response>
        [HttpPost("DeleteList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteListPostAsync([FromBody] IList<Application.Features.Models.Dto.MenuDto> listObjBase)
        {
            var validate = await _mediator.Send(new Application.Features.Commands.DeleteListEntitiesAsyncCommand<Application.Features.Models.Dto.MenuDto, Domain.AggregateModels.Menu> (listObjBase, nameof(Application.Features.Models.Dto.MenuDto.Path )));
            if (validate?.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, validate);
            }
            return ResultApiAsync(listObjBase[0]);
        }


        /// <summary>
        /// Deletes a record by [{id}] from Entity Menu
        /// </summary>
        /// <param name="id">id from the record</param>
        /// <returns>
        /// Returns an HTTP response with StatusCodes
        ///         200OK Operation response and ResponseApi Object
        ///         404NotFound If a record cannot be found for [{id}] to delete from the Entity
        ///         400BadRequest If a failure, validation or controlled error occurred
        ///         500InternalServerError If a failure or error occurred that was NOT controlled
        ///         403Forbidden If you do not have permissions to execute the action
        ///         401Unauthorized If you are not authenticated
        /// </returns>
        /// <remarks>
        /// Entity <ENTypeparamref name="DTO"/>
        /// </remarks>
        /// <response code="200">Operation response and ResponseApi Object</response>
        /// <response code="404">If a record cannot be found for [{id}] to delete from the Entity </response>
        /// <response code="400">If a failure, validation or controlled error occurred</response>
        /// <response code="500">If a failure or error occurred that was NOT controlled</response>
        /// <response code="403">If you do not have permissions to execute the action</response>
        /// <response code="401">If you are not authenticated</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async override  Task<IActionResult> Delete(string id)
        {
            var objBase = await _mediator.Send(new Application.Features.Queries.GetEntityAsyncById<Application.Features.Models.Dto.MenuDto, Domain.AggregateModels.Menu>(id) );
            return await DeleteListPostAsync(new List<Application.Features.Models.Dto.MenuDto>() {  objBase ?? new Application.Features.Models.Dto.MenuDto() { Id =-1,Path = string.Empty}}  );
        }



        /// <summary>
        /// Method to obtain the Menu by the Role Id.
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Role/{RoleId:int}")]
        public async  Task<List<Application.Features.Models.Dto.MenuDto>> MenuByRol(int RoleId)
        {
            object[] values = { 0 };
            ItemsFilters item = new ItemsFilters()
            {
                Name = nameof(Domain.AggregateModels.Menu.Id),
                Operator = EnumerationApplication.OperationExpression.NotEquals,
                Values = values
            };
            List<ItemsFilters> itemsFilters = new List<ItemsFilters>() { item };
            Filter objFilter = new Filter() { Filters = itemsFilters };
            return await _mediator.Send(new GetDataEntityAsync<Application.Features.Models.Dto.MenuDto, Domain.AggregateModels.Menu>(objFilter, TimeZone));                        
        }
    }
}
