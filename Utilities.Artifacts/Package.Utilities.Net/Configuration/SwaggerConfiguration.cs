namespace Package.Utilities.Net
{

    /// <summary>
    /// Clase para la extraccion de la configuracion del Swagger de las Apis
    /// </summary>
    public class SwaggerConfiguration
    {

        /// <summary>
        /// <para>App</para>
        /// </summary>
        public string App { get; set; }

        /// <summary>
        /// <para>Module</para>
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// <para>Functionality</para>
        /// </summary>
        public string Functionality { get; set; }

        /// <summary>
        /// <para>ResponsibleAnalyst</para>
        /// </summary>
        public string ResponsibleAnalyst { get; set; }

        /// <summary>
        /// <para>ResponsibleAnalyst</para>
        /// </summary>
        public string EmailResponsibleAnalyst { get; set; }

        /// <summary>
        /// <para>Foo API v1</para>
        /// </summary>
        public string EndpointDescription { get; set; }

        /// <summary>
        /// <para>/swagger/v1/swagger.json</para>
        /// </summary>
        public string EndpointSwaggerJson { get; set; }

        /// <summary>
        /// <para>/swagger</para>
        /// </summary>
        public string EndpointSwagger { get; set; }

        /// <summary>
        /// <para>/health</para>
        /// </summary>
        public string EndpointHealth { get; set; }

        /// <summary>
        /// <para>/api/discovery</para>
        /// </summary>
        public string EndpointDiscovery { get; set; }

        /// <summary>
        /// <para>[ContactName]</para>
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// <para>[ContactUrl]</para>
        /// </summary>
        public System.Uri ContactUrl { get; set; }

        /// <summary>
        /// <para>[ContactEmail]</para>
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// <para>API {Nombre Proyecto}</para>
        /// </summary>
        public string DocInfoTitle { get; set; }

        /// <summary>
        /// <para>v1</para>
        /// </summary>
        public string DocInfoVersion { get; set; }

        /// <summary>
        /// <para>v1</para>
        /// </summary>
        public string DocInfoVersionLatest { get; set; }

        /// <summary>
        /// <para>API {Nombre Proyecto} - Web API in ASP.NET 5.0</para>
        /// </summary>
        public string DocInfoDescription { get; set; }
    }
}
