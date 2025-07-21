using System;
using Package.Utilities.Net;

namespace Application.Features.Models.Dto
{
    public partial class MonedaDto
    {
        public string Id { get; set; }
        [ExcelPropertyAttribute("Codigo", 1, true)]
        public int Codigo { get; set; }
        public string Identificador { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTimeOffset? ActivoDesde { get; set; }
        public DateTimeOffset? ActivoHasta { get; set; }
        public bool Estado { get; set; }
    }
}
