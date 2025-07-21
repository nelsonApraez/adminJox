namespace Package.Utilities.Net
{
    using System.Collections.Generic;

    /// <summary>
    /// Clase encargada de Administar los textos de la aplicación de la forma definida por default
    /// </summary>
    internal abstract partial class TextResponseMessage
    {
        /// <summary>
        /// Metodo para obtener los textos de la aplicación de la forma definida por default
        /// </summary>
        /// <param name="cusMessage">Mensaje que se Retornara</param>
        /// <returns>Objeto TextResponse con las propiedades cargadas del mensaje configurado</returns>
        internal virtual TextResponse GetTextResponseMessage(EnumerationException.Message cusMessage)
        {
            var textAplication = TextResponseAplication.GetValueOrDefault(cusMessage);
            if (textAplication.Message.IsValid())
            {
                return textAplication;
            }

            return TextResponse.GetValueOrDefault(cusMessage);
        }

        /// <summary>
        /// Recupera el mensaje base que debe mostrar de acuerdo al tipo de proceso
        /// </summary>
        /// <param name="cusMessage"></param>
        /// <returns></returns>
        internal virtual string GetTextResourceMessage(string cusMessage)
        {
            return TextResponseApplication.Texts.Transversal.ResourceManager.GetString(cusMessage);
        }

        /// <summary>
        /// Mensajes configurado para la aplicación
        /// </summary>
        protected static Dictionary<EnumerationException.Message, TextResponse> TextResponse { get; } =
            new Dictionary<EnumerationException.Message, TextResponse>
        {
            {
                EnumerationException.Message.ErrorGeneral,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrorGeneral)
            },

            {
                EnumerationException.Message.MsjCreacion,
                new TextResponse(
                    TextResponseApplication.Texts.Transversal.MsjCreacionTitle,
                    TextResponseApplication.Texts.Transversal.MsjCreacionMessage,
                    TextResponseApplication.Texts.Transversal.MsjCreacionMessageNegative
                    )
            },
            {
                EnumerationException.Message.MsjActualizacion,
                new TextResponse(
                    TextResponseApplication.Texts.Transversal.MsjActualizacionTitle,
                    TextResponseApplication.Texts.Transversal.MsjActualizacionMessage,
                    TextResponseApplication.Texts.Transversal.MsjActualizacionMessageNegative
                    )
            },
            {
                EnumerationException.Message.MsjEliminacion,
                new TextResponse(
                    TextResponseApplication.Texts.Transversal.MsjEliminacionTitle,
                    TextResponseApplication.Texts.Transversal.MsjEliminacionMessage,
                    TextResponseApplication.Texts.Transversal.MsjEliminacionMessageNegative
                    )
            },

            {
                EnumerationException.Message.ErrorGeneralDB,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrorGeneralDB)
            },


            {
                EnumerationException.Message.ErrNoEncontrado,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrNoEncontrado)
            },
            {
                EnumerationException.Message.ErrRegistrosDependientes,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrRegistrosDependientes)
            },
            {
                EnumerationException.Message.ErrorTableNameAzureStorage,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrorTableNameAzureStorage)
            },
            {
                EnumerationException.Message.ErrorStorageConnectionAzureStorage,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrorStorageConnectionAzureStorage)
            },
            {
                EnumerationException.Message.ErrUniqueKey,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrUniqueKey)
            },
            {
                EnumerationException.Message.ErrRequiredField,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrRequiredField)
            },
            {
                EnumerationException.Message.ErrForeingkey,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrForeingkey)
            },
            {
                EnumerationException.Message.ErrMaxLength,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrMaxLength)
            },
            {
                EnumerationException.Message.ErrMaxValue,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrMaxValue)
            },
            {
                EnumerationException.Message.ExistField,
                new TextResponse(TextResponseApplication.Texts.Transversal.ExistField)
            },
            {
                EnumerationException.Message.ErrNoDataExport,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrNoDataExport)
            },
            {
                EnumerationException.Message.ErrNoColumnExport,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrNoColumnExport)
            },
            {
                EnumerationException.Message.ObjectEmpty,
                new TextResponse(TextResponseApplication.Texts.Transversal.ObjectEmpty)
            },
            {
                EnumerationException.Message.Unauthorized,
                new TextResponse(TextResponseApplication.Texts.Transversal.Unauthorized)
            },
            {
                EnumerationException.Message.KeyVaultSolicitudFallida,
                new TextResponse(TextResponseApplication.Texts.Transversal.KeyVaultSolicitudFallida)
            },
            {
                EnumerationException.Message.KeyVaultFallaGuardar,
                new TextResponse(TextResponseApplication.Texts.Transversal.KeyVaultFallaGuardar)
            },
            {
                EnumerationException.Message.KeyVaultArgumentosNulos,
                new TextResponse(TextResponseApplication.Texts.Transversal.KeyVaultArgumentosNulos)
            },
            {
                EnumerationException.Message.ErrorAuthorization,
                new TextResponse(TextResponseApplication.Texts.Transversal.ErrorAuthorization)
            }
        };
    }
}
