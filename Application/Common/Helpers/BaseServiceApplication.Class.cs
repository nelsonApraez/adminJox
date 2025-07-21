namespace Application.BaseApplicationHelper
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Application.Common.Commands;
    using Application.Features.Parametro.Command;
    using EventSourcingCore.Commands;
    using FluentValidation.Results;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Package.Utilities.Net;

    /// <summary>
    /// Clase base de negocio para todas las entidades de negocio
    /// </summary>
    public abstract partial class BaseServiceApplication : BaseMappingHelper
    {
        #region Propiedades
        /// <summary>
        /// Componente de comunicacion bidireccional de contextos de negocio
        /// </summary>
        protected readonly IMediator _mediator;

        /// <summary>
        /// Lista de validaciones genericas de negocio
        /// </summary>
        protected List<ResponseApi> ValidationsApi = new();

        /// <summary>
        /// Guardar Auditoria Transaccional
        /// </summary>
        protected bool WithAudit = true;

        /// <summary>
        /// Guardar Logs en Auditoria
        /// </summary>
        protected bool WithLogAudit = false;

        /// <summary>
        /// Publica la operacion a los subdominios interesados
        /// </summary>
        protected bool WithPubEvent = false;


        /// <summary>
        /// Nombre de la Clase Referenciada trabajada
        /// </summary>
        public string NameClassReference { get; }

        /// <summary>
        /// Setear valor para Guardar Auditoria Transaccional
        /// </summary>
        public void SetWithAudit(bool value)
        {
            WithAudit = value;
        }

        /// <summary>
        /// Setear valor para Guardar Logs en Auditoria
        /// </summary>
        public void SetWithLogAudit(bool value)
        {
            WithLogAudit = value;
        }

        /// <summary>
        /// Constructor de la Clase <BaseServiceApplication></BaseServiceApplication>
        /// </summary>
        /// <param name="mediator"></param>
        protected BaseServiceApplication(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Constructor de la Clase <BaseServiceApplication></BaseServiceApplication>
        /// </summary>
        /// <param name="classBussiness"></param>
        /// <param name="mediator"></param>
        protected BaseServiceApplication(string classBussiness, IMediator mediator)
        {
            NameClassReference = classBussiness;
            _mediator = mediator;
        }
        #endregion

        #region Validaciones
        /// <summary>
        /// Validacion de errores de modelo de negocio
        /// </summary>
        /// <param name="validation"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool AddValidation(ValidationResult validation, object obj)
        {
            validation.Errors.ForEach(item =>
            {
                AddValidation(StatusCodes.Status400BadRequest, item.ErrorCode, string.Join('|', item.ErrorMessage, item.PropertyName).Split('|'), obj);
            });
            return validation.IsValid;
        }


        /// <summary>
        /// Creacion de validacion de reglas de metadatos o negocio
        /// </summary>
        /// <param name="statusCodes">StatusCodes Response</param>
        /// <param name="message">EnumerationMessage control operation</param>
        /// <param name="obj">Instance object context</param>
        /// <returns></returns>

        internal void AddValidation(int statusCodes, string message, string[] tags, object obj, string resourceName = "")
        {
            //se recuperan los mensajes del conector del componente de mensajes
            if (string.IsNullOrEmpty(resourceName))
                resourceName = $"{ResponseApi.GetTypeMessage(statusCodes)}.{NameClassReference}.{message}";
            ValidationsApi.Add(Task.Run(async () => await _mediator.Send(new TextMessageCommand(statusCodes, EnumerationException.Message.MessageGeneral, tags, ResponseApi.GetTypeMessage(statusCodes), obj, resourceName))).GetAwaiter().GetResult());
        }

        /// <summary>
        /// Metodo que genera las respuestas de acuerdo a la eliminacion parcial o generarl de las Entidades
        /// </summary>
        /// <param name="dependents">Empresas no eliminadas por dependencias hacia otros elementos del dominio</param>
        /// <param name="notexist">Empresas que no existen en la eliminacion</param>
        /// <param name="deletes">Empreas que fueron eliminadas satisfactoriamente</param>
        protected void ResponseForDeleteEntity(List<string> dependents, List<string> notexist, List<string> deletes)
        {
            //mensajes de respuesta
            if (dependents.Count > 0 || notexist.Count > 0)
            {
                if (dependents.Count == 0)
                {
                    AddValidation(StatusCodes.Status400BadRequest, $"{EnumerationMessage.Message.MsjEliminacion}", string.Join(", ", notexist).Split('|'), null);
                }
                else
                {
                    if (notexist.Count == 0)
                    {
                        AddValidation(StatusCodes.Status400BadRequest, $"{EnumerationMessage.Message.ErrForeingkey}", (string.Join(", ", dependents)).Split('|'), null);
                    }
                    else
                    {
                        AddValidation(StatusCodes.Status400BadRequest, $"{EnumerationMessage.Message.Dependiente}", (string.Join(", ", dependents) + "|" + string.Join(", ", notexist)).Split('|'), null);
                    }
                }
            }
            else
            {
                AddValidation(StatusCodes.Status200OK, $"{EnumerationMessage.Message.MsjEliminacion}", string.Join(", ", deletes).Split('|'), null);
            }
        }

        #endregion

        #region Captura de Excepciones

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="fnAccion"></param>
        /// <returns></returns>
        public TReturn ExceptionBehavior<TReturn>(Func<TReturn> fnAccion)
        {
            TReturn returnBehavior;

            try
            {
                returnBehavior = fnAccion();
            }
            catch (Exception ex)
            {
                returnBehavior = default;
                HandleExceptions(ex).ConfigureAwait(false);
            }

            return returnBehavior;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="fnAccion"></param>
        /// <returns></returns>
        public async Task<TReturn> ExceptionBehaviorAsync<TReturn>(Func<Task<TReturn>> fnAccion)
        {
            TReturn returnBehavior;

            try
            {
                returnBehavior = await fnAccion().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                returnBehavior = default;
                await HandleExceptions(ex);
            }

            return returnBehavior;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        protected async Task HandleExceptions(Exception ex)
        {
            if (WithLogAudit)
            {
                //implement register trazability log
            }

            if (ex?.GetType()?.Equals(typeof(CustomException)) == true &&
                (((CustomException)ex).TypeException == EnumerationException.TypeCustomException.Validation))
            {
                throw ex;
            }

            ExceptionProcessorHelper.HandleException(ex);
        }
        #endregion

        #region Auditoria


        /// <summary>
        /// Metodo para la creacion de la auditorias transaccional (Creación, Modificación, Eliminación)
        /// </summary>
        /// <typeparam name="TA">Objeto Transaccional</typeparam>
        /// <param name="objActual">Objeto Actual Transaccional</param>
        /// <param name="objAnterior">Objeto Anterior Transaccional</param>
        /// <param name="eAcciones">Acción a Realizar (Creación, Modificación, Eliminación)</param>
        public virtual async Task CreateAudit<TA>(TA objActual, TA objAnterior, EnumerationApplication.Operations eAcciones, string className = "")
            where TA : class
        {
            var content = new
            {
                ObjectCurrent = objActual,
                NameClassReference
            };
            if (WithAudit)
            {
                try
                {
                    //implementa register audit
                    System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(content));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }

            if (WithPubEvent)
            {
                await _mediator.Send(new EventPublishedAsync(new EventSourcingCore.Class.EventBusiness() { IdOperation = $"{eAcciones}", IdTaskingReference = Guid.NewGuid().ToString(), Data = JsonSerializer.Serialize(content) }));
            }
        }

        /// <summary>
        /// Metodo para la creacion de log de auditorias para controlar
        /// </summary>
        /// <param name="category">Categoria de la excepcion</param>
        /// <param name="textAudit">texto para el registro de los logs</param>
        public virtual async Task CreateLogAudit(EnumerationException.CategoryException category, string textAudit)
        {
            if (WithLogAudit)
            {
                try
                {
                    //implementa log Audit
                    var content = new
                    {
                        Category = category,
                        TextCategory = "CreateLogAudit",
                        Text = textAudit
                    };
                    System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(content));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }

        #endregion

        #region publicar Eventos Dominio
        protected async Task PublishDomainEvent<T>(T obj, string state, string idReference)
            => await _mediator.Send(new PublishEventDomainCommand(new Domain.Common.DomainEventDispatcher<T>(obj, state, idReference)));
        #endregion
    }
}
