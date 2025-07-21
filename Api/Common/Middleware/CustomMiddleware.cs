namespace Api.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Api.Controllers;
    using Microsoft.AspNetCore.Http;
    using Package.Utilities.Net;

    /// <summary>
    /// Middleware personalizado
    /// </summary>
    public class CustomMiddleware
    {
        /// <summary>
        /// Objeto con la información del request
        /// </summary>
        private readonly RequestDelegate _request;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="request">peticion realizada</param>        
        public CustomMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //implement autorization external service
                await _request(httpContext);
            }
            catch (Exception ex)
            {
                ExceptionResultApi(httpContext, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="excepcion"></param>
        protected void ExceptionResultApi(HttpContext context, Exception excepcion)
        {
            context.Response.ContentType = Constants.ContentType;
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            string detalle = $"Error: | {excepcion?.Message} | Detalle: | {excepcion?.InnerException?.Message} | Traza: | {excepcion?.StackTrace}";

            var objectResponse = ResultResponseApi(StatusCodes.Status500InternalServerError, EnumerationMessage.Message.ErrorGeneral, null, detalle);
            if (excepcion?.GetType() == typeof(CustomException))
            {
                var customException = (CustomException)excepcion;
                context.Response.StatusCode = BaseUtilities.StatusCodeRequest(customException.TypeException);
                if (customException.IsNotNull())
                {
                    objectResponse = ResultResponseApi(BaseUtilities.StatusCodeRequest(customException.TypeException),
                                             customException.ErrorBusiness,
                                             customException.TagTextBusiness,
                                              context.Response.StatusCode == StatusCodes.Status401Unauthorized ? excepcion.Message : detalle);
                }
            }

            if (excepcion?.InnerException?.GetType() == typeof(CustomException))
            {
                var customException = (CustomException)excepcion.InnerException;
                context.Response.StatusCode = BaseUtilities.StatusCodeRequest(customException.TypeException);
                if (customException.IsNotNull())
                {
                    objectResponse = ResultResponseApi(BaseUtilities.StatusCodeRequest(customException.TypeException),
                                             customException.ErrorBusiness,
                                             customException.TagTextBusiness,
                                             detalle);
                }
            }
            context.Response.WriteAsync(objectResponse.ToJsonSerialize());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCodes"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        private static ResponseApi ResultResponseApi(int statusCodes, EnumerationException.Message message, string[] tags, string detail = null)
        {
            return new ResponseApi(message, tags, detail, BaseUtilities.GetTypeMessage(statusCodes));
        }
    }
}
