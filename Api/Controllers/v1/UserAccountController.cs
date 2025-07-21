namespace Api.Controllers
{
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;    
    using Application.Features.Models.Dto;    
    using Domain.AggregateModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Package.Utilities.Net;
    using Application.Features.Queries;
    using Package.Utilities.Net.Utilities;
    using Application.Features.Commands;
    using Application.Models;

    /// <summary>
    /// API to expose the methods for the entity [User]
    /// </summary>
    public partial class UserAccountController : BaseControllerDm<Application.Features.Models.Dto.UserDto, Domain.AggregateModels.User>
    {
        // <summary>
        /// Login user.        
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<LoginOutDto> Login([FromBody] LoginRequest request)
        {
            //UserDto result = await _mediator.Send(new LoginAsyncCommand<UserDto, User>(request));
            object[] values = { request.User };
            ItemsFilters item = new ItemsFilters()
            {
                Name = nameof(Domain.AggregateModels.User.Username) ,
                Operator = EnumerationApplication.OperationExpression.Equals,
                Values = values
            };
            LoginOutDto response = new LoginOutDto();
            List<ItemsFilters> itemsFilters = new List<ItemsFilters>() { item };
            Filter objFilter = new Filter() { Filters = itemsFilters };
            var resultls = await _mediator.Send(new GetDataEntityAsync<UserDto, Domain.AggregateModels.User>(objFilter, TimeZone));
            if (resultls.Count > 0)
            {
                var result = resultls[0];
                response.FullName = result.FullName;
                response.RoleId = (int)result.RoleId;
                response.RoleName = result.RoleName;
                response.Token = SecurityToken.GenerateTokenAuthentication(result.Id.ToString(), result.Email, result.FullName, result.Username);
            }
            return response;
        }

        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>User</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto request)
        {            
            // Get Role by Name 'Guest'
            object[] values = { "Guest" };
            ItemsFilters item = new ItemsFilters()
            {
                Name = "Name",
                Operator = EnumerationApplication.OperationExpression.Equals,
                Values = values
            };

            List<ItemsFilters> itemsFilters = new List<ItemsFilters>() { item };
            Filter objFilter = new Filter() { Filters = itemsFilters };
            List<RoleDto> roleDto = await _mediator.Send(new GetDataEntityAsync<RoleDto, Role>(objFilter, TimeZone));
            //todo: validar el perfil que se envia
            request.RoleId = roleDto.FirstOrDefault().Id;            
            return await PostAsync(request);
        }

        /// <summary>
        /// Validates a session token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>User</returns>
        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<UserDto> ValidateToken([FromBody] String token)
        {
            string id = SecurityToken.ValidateTokenAuthentication(token);
            
            UserDto userDto = await _mediator.Send(new GetEntityAsyncById<UserDto, User>(id));

            return userDto;
        }

        /// <summary>
        /// Sets a new language.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns>IActionResult</returns>
        [AllowAnonymous]
        [HttpPost("language")]
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture))
            );
            return Ok();
        }
    }
}
