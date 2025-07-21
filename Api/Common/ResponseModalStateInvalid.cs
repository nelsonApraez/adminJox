namespace Web.Api.Base.Api
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Clase para encapsular las validaciones personalizadas del Model de las Peticiones
    /// </summary>
    public static class ResponseModalStateInvalid
    {
        /// <summary>
        /// Metodo para customizar las respuesta del validate del model.
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        public static IActionResult GetInvalidModalState(ActionContext actionContext)
        {
            var modalState = actionContext.ModelState.Values;
            return new BadRequestObjectResult(modalState);
        }
    }
}
