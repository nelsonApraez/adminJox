namespace Application.Features.Services
{
    using Package.Utilities.Net;
    using System;
    using MediatR;
    using Application.Models.Validators;
    using Domain.AggregateModels;
    using System.Threading.Tasks;
    using Domain.AggregateModels.Specs;    
    using System.IO;
    using System.Linq.Dynamic.Core;    
    using System.Reflection.Metadata;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;
    using Document = QuestPDF.Fluent.Document;
    using Application.Features.Interfaces;
    using CSharpFunctionalExtensions;
    using System.Linq;
    using Unit = QuestPDF.Infrastructure.Unit;
    using Domain.Services.Interfaces;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Clase representa el negocio para la Entidad (Processingresult)
    /// </summary>
    [BusinessAttribute]
    public partial class ProcessingresultService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Domain.AggregateModels.Processingresult>,
        Interfaces.IProcessingresultService
    {
        private IPromptService _promtservice;
        private IProyectoService _proyectoservice;
        private IragIAServices _ragservice;

        /// <summary>
        /// Constructor para inicializar la capa de acceso a datos, Instacia del Contexto [Processingresult].
        /// </summary>
        /// <param name="repositoryContext">Instacia del Contexto a Base de Datos</param>
        public ProcessingresultService(Domain.Repositories.Interfaces.IProcessingresultRepository repositoryContext, IMediator mediator,
            IPromptService promptService,IProyectoService proyectoService, IragIAServices ragService) :
            base(repositoryContext, mediator) 
        {
           OrderDefaultEntity = nameof(Application.Features.Models.Dto.ProcessingresultDto.Proyectoid);
           CreateMapperExpresion<Application.Features.Models.Dto.ProcessingresultDto, Domain.AggregateModels.Processingresult>(cnf => {
               ProcessingresultMapper.Expresion(cnf);
               _promtservice = promptService;
               _proyectoservice = proyectoService;
               _ragservice = ragService;
           });
        }

        public async Task<byte[]> GetProcessingFileByProject(Guid idProyecto)
        {
            //get info solution 
            var proyecto = await _proyectoservice.GetByIdAsync(idProyecto.ToString());
            var response = await GetProcessingresultByProject(idProyecto);
            var listPromts = await _promtservice.ToListAsync();
            var promtSolution = listPromts.Where(x => x.Nombre.Valor != "Solucion").ToList();
            var contentpdf = new Dictionary<string, string>();
            foreach (var promp in promtSolution)
            {
                var promptsolution = promp.Promtuser.Valor.Replace("{0}", response.Suggestedsolution);
                var iareponse = await _ragservice.GetMessageRagAsync(promptsolution, promp);
                if (iareponse == null)
                    continue;
                contentpdf.Add(promp.Nombre.Valor, iareponse);
            }
            QuestPDF.Settings.License = LicenseType.Community;
            //create pdf file 
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(16));
                    page.Header()
                        .AlignCenter()
                        .Text("Software ONE")
                        .SemiBold().FontSize(24).FontColor(Colors.Grey.Darken4);

                    page.Content()
                         .PaddingVertical(1, Unit.Centimetre)
                         .Column(x =>
                         {
                             x.Spacing(20);
                             x.Item().Text(proyecto.Nombre.Valor);
                             x.Spacing(20);
                             x.Item().Text(proyecto.Descripcion.Valor);
                             x.Spacing(20);
                             x.Item().Text("Propuesta");
                             x.Spacing(20);
                             x.Item().Text(response.Suggestedsolution);
                             x.Spacing(20);
                             foreach (var promp in contentpdf)
                             {
                                 x.Spacing(20);
                                 x.Item().Text(promp.Key);
                                 x.Spacing(20);
                                 x.Item().Text(promp.Value);
                             }
                         });                    

                                     
                });
            })
            .GeneratePdf();
        }

        public async Task<Application.Features.Models.Dto.ProcessingresultDto> GetProcessingresultByProject(Guid idProyecto)
        {
            var temp = await Repository.SearchAsync(ProcessingresultSpecification.ProcessingresultByIdProject(idProyecto));
            return MapObj<Processingresult, Application.Features.Models.Dto.ProcessingresultDto>(temp);
        }
    }
}
