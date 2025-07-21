namespace Api.Controllers
{
    using System;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Package.Utilities.Net;

    ////[Authorize]   
    /// <summary>
    /// BaseController Base de implementacion abstracta 
    /// </summary>
    /// <typeparam name="DTO">Generic abstracto para el uso del patron DTO</typeparam>
    /// <typeparam name="ENT">Generic abstracto para el uso del patron Aggregate </typeparam>
    public abstract partial class BaseControllerDm<DTO, ENT>
    {




        /// <summary>
        /// Constructor para inicializar la base con el ServiceApplication de la entidad
        /// </summary>
        /// <param name="mediator"></param>        
        protected BaseControllerDm(IMediator mediator) : base(mediator)
        {
            NameClassReference = typeof(ENT).Name;
        }



        /// <summary>
        /// Metodo para encapsular y procesar los valores que se retornaran
        /// </summary>
        /// <param name="objEntidad">Objeto de la entidad que se esta procesando</param>
        /// <returns>Retorna un vector con los valores asocados</returns>
        protected virtual string[] GetParameters<T>(T objEntidad) => default;


        /// <summary>
        /// Metodo para encapsular y procesar el objeto que se retornara
        /// </summary>
        /// <param name="objEntidad">Objeto de la entidad que se esta procesando</param>
        /// <returns>Retorna un vector con los valores asocados</returns>
        protected virtual object GetParametersObj<T>(T objEntidad) => objEntidad;


        protected IActionResult ExceptionResultApi(HttpContext context, Exception excepcion)
        {
            context.Response.ContentType = Constants.ContentType;
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var objectResponse = ResultResponseApi(StatusCodes.Status500InternalServerError, EnumerationMessage.Message.ErrorGeneral, null);
            if (excepcion.GetType() == typeof(CustomException) || excepcion.InnerException.GetType() == typeof(CustomException))
            {
                var customException = (CustomException)(excepcion.GetType() == typeof(CustomException) ? excepcion : excepcion.InnerException);
                context.Response.StatusCode = StatusCodeRequest(customException.TypeException);
                if (customException.IsNotNull())
                {
                    objectResponse = StatusCode(context.Response.StatusCode, new ResponseApi(EnumerationMessage.Message.Unauthorized, customException.Message,
                        new DetailResponseApi(EnumerationMessage.Message.Unauthorized, null, GetTypeMessage(context.Response.StatusCode),
                        new TextResponse("", "", ""))
                        {
                            Detail = customException.Message,
                        }
                    ));
                }
            }
            return objectResponse;
        }
    }
}
