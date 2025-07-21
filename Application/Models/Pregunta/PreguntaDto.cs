namespace Application.Features.Models.Dto
{
    /// <summary>
    /// Esta clase representa la Implementacion DTO para la Entidad (Pregunta)
    /// </summary>
    public partial class PreguntaDto
    {
        public string Id { get; set; }

        /// </summary>
        public string Valor { get; set; }

        public string Descripcion { get; set; }

        public int NumeroCategoria { get; set; }

        public string NombreCategoria { get; set; }
    }

}
