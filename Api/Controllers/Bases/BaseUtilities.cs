namespace Api.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Package.Utilities.Net;



    /// <summary>
    /// Clase base de utilidades de los controllers de la api
    /// </summary>
    public abstract partial class BaseUtilities : Controller
    {
        /// <summary>
        /// Activar la validación de las acciones con autorizacion
        /// </summary>
        protected bool ValidAuthorization = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCodes"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        protected IActionResult ResultResponseApi(int statusCodes, EnumerationException.Message message, string[] tags)
        {
            return StatusCode(statusCodes, DigiToolsMessage.TextMessageApplication.TextMessageApi.BuildResponseApi(statusCodes, message, tags, ResponseApi.GetTypeMessage(statusCodes)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCodes"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected IActionResult ResultResponseApi(int statusCodes, string message, string[] tags, object obj)
        {
            return StatusCode(statusCodes, DigiToolsMessage.TextMessageApplication.TextMessageApi.BuildResponseApi(statusCodes, EnumerationException.Message.MessageGeneral, tags, ResponseApi.GetTypeMessage(statusCodes), obj, $"{ResponseApi.GetTypeMessage(statusCodes)}.{NameClassReference}.{message}"));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCodes"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected IActionResult ResultResponseApi(int statusCodes, EnumerationException.Message message, string[] tags, object obj)
        {
            return StatusCode(statusCodes, DigiToolsMessage.TextMessageApplication.TextMessageApi.BuildResponseApi(statusCodes, message, tags, ResponseApi.GetTypeMessage(statusCodes), obj, $"{ResponseApi.GetTypeMessage(statusCodes)}.{NameClassReference}.{message}"));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCodes">StatusCodes Response</param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        protected IActionResult ResultApi(int statusCodes, EnumerationException.Message message, string[] tags)
        {
            return ResultResponseApi(statusCodes, message, tags);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCodes"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected IActionResult ResultApi(int statusCodes, EnumerationException.Message message, string[] tags, object obj)
        {
            return ResultResponseApi(statusCodes, message, tags, obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCodes">StatusCodes Response</param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult ResultApi(int statusCodes, EnumerationException.Message message)
        {
            return ResultApi(statusCodes, message, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCodes"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        protected IActionResult ResultApi(bool statusCodes, EnumerationException.Message message, params string[] tags)
        {
            var statusSuccess = ((message == CusMessageCreate) ?
                                            StatusCodes.Status201Created :
                                            StatusCodes.Status200OK);

            return ResultResponseApi((statusCodes) ?
                                        statusSuccess :
                                        StatusCodes.Status400BadRequest, message, tags);
        }

        protected IActionResult ResultApi(bool statusCodes, EnumerationMessage.Message message, string[] tags, object obj)
        {
            var statusSuccess = ((message == CusMessageCreate) ?
                                            StatusCodes.Status201Created :
                                            StatusCodes.Status200OK);

            return ResultResponseApi((statusCodes) ?
                                        statusSuccess :
                                        StatusCodes.Status400BadRequest, message, tags, obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCodes"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult ResultApi(bool statusCodes, EnumerationException.Message message)
        {
            return ResultApi(statusCodes, message, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objResult"></param>
        /// <returns></returns>
        protected IActionResult ResultApi<T>(T objResult) => StatusCode(StatusCodes.Status200OK, objResult);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objResult"></param>
        /// <returns></returns>
        protected IActionResult ResultApiAsync<T>(T objResult)
        {
            return StatusCode(StatusCodes.Status200OK, objResult);
        }


        /// <summary>
        /// Validacion de resupuesta para las reglas de negocio
        /// </summary>
        /// <param name="validateRules"></param>
        /// <returns></returns>
        protected IActionResult ResultApiValidationRules(List<ResponseApi> validateRules)
        {
            return StatusCode((int)validateRules[0].InnerMessage.TypeMessage, validateRules[0]);
        }



        /// <summary>
        /// Categorización de StatusCode
        /// </summary>
        /// <param name="statusCodes"></param>
        /// <returns></returns>
        public static EnumerationApplication.TypeMessage GetTypeMessage(int statusCodes)
        {
            return statusCodes switch
            {
                StatusCodes.Status200OK => EnumerationApplication.TypeMessage.Success,
                StatusCodes.Status500InternalServerError => EnumerationApplication.TypeMessage.Error,
                StatusCodes.Status400BadRequest => EnumerationApplication.TypeMessage.Warning,
                StatusCodes.Status401Unauthorized => EnumerationApplication.TypeMessage.Unauthorized,
                _ => EnumerationApplication.TypeMessage.Alert
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeCustomException"></param>
        /// <returns></returns>
        public static int StatusCodeRequest(EnumerationException.TypeCustomException typeCustomException)
        {
            return typeCustomException switch
            {
                EnumerationException.TypeCustomException.Validation => StatusCodes.Status400BadRequest,
                EnumerationException.TypeCustomException.BusinessException => StatusCodes.Status500InternalServerError,
                EnumerationException.TypeCustomException.NoContent => StatusCodes.Status204NoContent,
                EnumerationException.TypeCustomException.Unauthorized => StatusCodes.Status401Unauthorized,
                EnumerationException.TypeCustomException.Undefined => StatusCodes.Status406NotAcceptable,
                _ => StatusCodes.Status500InternalServerError,
            };
        }

        /// <summary>
        /// Método para requerir autorización de operaciones entidad
        /// </summary>
        /// <param name="eAcciones">Operación a validar</param>
        /// <returns>Valor lógico si tiene permisos sobre la operación</returns>
        protected void ValidateAuthorizationPermissions(EnumerationApplication.Operations eAcciones)
        {
            ValidateAuthorizationPermissions(EnumerationApplication.Validations.Entity, eAcciones, null);
        }

        /// <summary>
        /// Método para requerir autorización de operaciones entidad
        /// </summary>
        /// <param name="nameValidation">Item a validar la Operación de EnumerationApplication.Operations.EjectAction</param>
        /// <returns>Valor lógico si tiene permisos sobre la operación</returns>
        protected void ValidateAuthorizationPermissionsButton(string nameValidation)
        {
            ValidateAuthorizationPermissions(EnumerationApplication.Validations.Button,
                                                  EnumerationApplication.Operations.Disable,
                                                  nameValidation);
        }

        /// <summary>
        /// Método para requerir autorización de operaciones entidad
        /// </summary>
        /// <param name="eValidations">Tipo de Validación</param>
        /// <param name="eAcciones">Operación a validar</param>
        /// <param name="nameValidation">Item a validar la Operación de EnumerationApplication.Operation</param>
        /// <returns>Valor lógico si tiene permisos sobre la operación</returns>
        private void ValidateAuthorizationPermissions(EnumerationApplication.Validations eValidations,
                                                      EnumerationApplication.Operations eAcciones,
                                                      string nameValidation)
        {
            if (ValidAuthorization)
            {
                throw new CustomException(string.Format(Constants.StaticMessage.LaValidacionDeOperacionNoEstaImplemantada, eValidations,
                eAcciones, nameValidation));
            }
        }

    }
}
