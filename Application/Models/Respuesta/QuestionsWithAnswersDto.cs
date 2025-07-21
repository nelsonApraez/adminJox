using System;

namespace Application.Models.Respuesta
{
    public class QuestionsWithAnswersDto
    {
        public Guid PreguntaId { get; set; }
        public string Pregunta { get; set; }
        public Guid RepuestaId { get; set; }
        public string Repuesta { get; set; }
        public int NumeroCategoria { get; set; }
        public string NombreCategoria { get; set; }
    }
}
