using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Domain.Common.Enums;
using Infrastructure.Integration.DigiTools.Message.TextMessageApplication.Texts;
using Package.Utilities.Net;

namespace DigiToolsMessage.TextMessageApplication
{
    /// <summary>
    /// Clase encargada de Orquestar los textos de respuesta de la api
    /// </summary>
    public static class TextMessageApi
    {

        /// <summary>
        /// Contructor que permite la generacion de ResponseApi
        /// </summary>
        /// <param name="message">Codigo del mensaje a retornar</param>
        /// <param name="tags">Tags de reemplazo en el Menssage</param>
        /// <param name="TypeMessage">Categorización del Error</param>
        public static ResponseApi BuildResponseApi(int status, EnumerationException.Message message,
                           string[] tags,
                           EnumerationApplication.TypeMessage TypeMessage)
        {
            var approvedMessage = TextMessageResponse.GetValueOrDefault(message);
            var InnerMessage = new DetailResponseApi(message, tags, TypeMessage, approvedMessage);
            var Message = approvedMessage.Message;
            if (TypeMessage != EnumerationApplication.TypeMessage.Success && approvedMessage.NegativeMessage.IsValid())
            {
                Message = approvedMessage.NegativeMessage;
            }

            if (Message.IsValid() && tags.IsNotNull())
            {
                Message = string.Format(CultureInfo.CurrentCulture, Message, tags);
            }
            return new ResponseApi(message, Message, InnerMessage);
        }


        /// <summary>
        /// Contructor que permite la generacion de ResponseApi
        /// </summary>
        /// <param name="message">Codigo del mensaje a retornar</param>
        /// <param name="tags">Tags de reemplazo en el Menssage</param>
        /// <param name="typeMessage">Categorización del Error</param>
        /// <param name="resourceName">Nombre de recurso adiconal para completar el contenido del mensaje</param>
        public static ResponseApi BuildResponseApi(int status, EnumerationException.Message message,
                           string[] tags,
                           EnumerationApplication.TypeMessage typeMessage, object obj, string resourceName)
        {
            var responseapi = BuildResponseApi(status, message, tags, typeMessage);
            var innerMessage = responseapi.InnerMessage;
            innerMessage.Detail = GetTextCustom(resourceName);
            if (!string.IsNullOrEmpty(innerMessage.Detail))
            {
                var responsemessage = JsonSerializer.Deserialize<ReponseMessageApi>(innerMessage.Detail);
                if (responsemessage != null)
                {
                    innerMessage.Detail = "";
                    innerMessage.TitleTag = "";
                    innerMessage.MessageTag = "";
                    //identifica los parametros de titulo
                    var tagsparams = GetParametersByObject(responsemessage.Parameters, tags, obj);
                    innerMessage.Title = string.Format(CultureInfo.CurrentCulture, responsemessage.Title, tagsparams);
                    //identifica los parametros de mensaje
                    innerMessage.Detail = string.Format(CultureInfo.CurrentCulture, responsemessage.Message, tagsparams);
                }

            }
            else
            {
                innerMessage.Title = resourceName;
                innerMessage.Detail = resourceName.Split('.').LastOrDefault() + " " + string.Join(" ", tags != null ? tags?.Distinct().ToList() : new List<string>());
            }
            return new ResponseApi(message, responseapi.Message, innerMessage);
        }


        /// <summary>
        /// Retorna los parametros del objetos por el tag dado
        /// </summary>
        /// <param name="parameter">Lista de parametros que debe buscar en el objeto</param>
        /// <param name="obj">Instancia del objeto donde buscara los parametros</param>
        /// <returns></returns>
        public static string[] GetParametersByObject(string[] parameter, string[] tags, object obj)
        {
            List<string> propsvalues = new();
            IList<PropertyInfo> props = obj != null ? new List<PropertyInfo>(obj.GetType().GetProperties()) : null;
            if (parameter != null)
            {
                foreach (var item in parameter.ToList())
                {
                    bool hasProperty = item.ToLower().Contains("$") && obj != null && props.Any(x => x.Name.ToLower() == item.ToLower().Replace("$", string.Empty));

                    string propertyValue = GetPropertyValue(hasProperty ? item.Replace("$", string.Empty) : string.Empty, obj);

                    propsvalues.Add(hasProperty && propertyValue != null ? propertyValue : item);
                }
            }
            if (tags != null)
                propsvalues.AddRange(tags.ToList());
            return propsvalues.ToArray();
        }

        private static string GetPropertyValue(string propertyName, object item)
        {
            var value = item.GetType()?.GetProperty(propertyName)?.GetValue(item, null);

            if (item.GetType()?.GetProperty(propertyName)?.PropertyType?.BaseType?.Name?.Contains(ValueObjectEnum.VALUE_OBJECT) == true)
            {
                value = value?.GetType()?.GetProperty(ValueObjectEnum.VALOR)?.GetValue(value, null);
            }

            return value?.ToString();
        }

        /// <summary>
        /// Retirna el mensaje customizado de procesamiento de entidades de negocio
        /// </summary>
        /// <param name="cusMessage"></param>
        /// <returns></returns>
        internal static string GetTextCustom(string cusMessage)
        {
            return Transversal.ResourceManager.GetString(cusMessage);
        }

        private static Dictionary<EnumerationException.Message, TextResponse> TextMessageResponse { get; } =
            new Dictionary<EnumerationException.Message, TextResponse>
        {
            {
                EnumerationException.Message.ErrorGeneral,
                new TextResponse(Transversal.ErrorGeneral)
            },

            {
                EnumerationException.Message.MsjCreacion,
                new TextResponse(
                    Transversal.MsjCreacionTitle,
                    Transversal.MsjCreacionMessage,
                    Transversal.MsjCreacionMessageNegative
                    )
            },
            {
                EnumerationException.Message.MsjActualizacion,
                new TextResponse(
                    Transversal.MsjActualizacionTitle,
                    Transversal.MsjActualizacionMessage,
                    Transversal.MsjActualizacionMessageNegative
                    )
            },
            {
                EnumerationException.Message.MsjEliminacion,
                new TextResponse(
                    Transversal.MsjEliminacionTitle,
                    Transversal.MsjEliminacionMessage,
                    Transversal.MsjEliminacionMessageNegative
                    )
            },

            {
                EnumerationException.Message.ErrorGeneralDB,
                new TextResponse(Transversal.ErrorGeneralDB)
            },


            {
                EnumerationException.Message.ErrNoEncontrado,
                new TextResponse(Transversal.ErrNoEncontrado)
            },
            {
                EnumerationException.Message.ErrRegistrosDependientes,
                new TextResponse(Transversal.ErrRegistrosDependientes)
            },
            {
                EnumerationException.Message.ErrorTableNameAzureStorage,
                new TextResponse(Transversal.ErrorTableNameAzureStorage)
            },
            {
                EnumerationException.Message.ErrorStorageConnectionAzureStorage,
                new TextResponse(Transversal.ErrorStorageConnectionAzureStorage)
            },
            {
                EnumerationException.Message.ErrUniqueKey,
                new TextResponse(Transversal.ErrUniqueKey)
            },
            {
                EnumerationException.Message.ErrRequiredField,
                new TextResponse(Transversal.ErrRequiredField)
            },
            {
                EnumerationException.Message.ErrForeingkey,
                new TextResponse(Transversal.ErrForeingkey)
            },
            {
                EnumerationException.Message.ErrMaxLength,
                new TextResponse(Transversal.ErrMaxLength)
            },
            {
                EnumerationException.Message.ErrMaxValue,
                new TextResponse(Transversal.ErrMaxValue)
            },
            {
                EnumerationException.Message.ExistField,
                new TextResponse(Transversal.ExistField)
            },
            {
                EnumerationException.Message.ErrNoDataExport,
                new TextResponse(Transversal.ErrNoDataExport)
            },
            {
                EnumerationException.Message.ErrNoColumnExport,
                new TextResponse(Transversal.ErrNoColumnExport)
            },
            {
                EnumerationException.Message.ObjectEmpty,
                new TextResponse(Transversal.ObjectEmpty)
            },
            {
                EnumerationException.Message.Unauthorized,
                new TextResponse(Transversal.Unauthorized)
            },
            {
                EnumerationException.Message.KeyVaultSolicitudFallida,
                new TextResponse(Transversal.KeyVaultSolicitudFallida)
            },
            {
                EnumerationException.Message.KeyVaultFallaGuardar,
                new TextResponse(Transversal.KeyVaultFallaGuardar)
            },
            {
                EnumerationException.Message.KeyVaultArgumentosNulos,
                new TextResponse(Transversal.KeyVaultArgumentosNulos)
            },
            {
                EnumerationException.Message.ErrorAuthorization,
                new TextResponse(Transversal.ErrorAuthorization)
            }
        };
    }
}
