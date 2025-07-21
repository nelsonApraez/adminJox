namespace Package.Utilities.Net
{
    using System.ComponentModel;

    /// <summary>
    /// Enumeraciones de Configuración de Mensajes de la aplicación
    /// </summary>
    public abstract class EnumerationMessage
    {
        /// <summary>
        /// Enumeracion con los Mensajes Definidos
        /// </summary>
        public enum Message
        {
            //// Transversales -1 - 10X
            [Description("Error General Aplicación")]
            ErrorGeneral = -1,

            MsjCreacion = 1,
            MsjActualizacion = 2,
            MsjEliminacion = 3,

            ErrNoEncontrado = 10,
            ErrRegistrosDependientes = 11,
            ErrorTableNameAzureStorage = 12,
            ErrorStorageConnectionAzureStorage = 13,
            ErrNoDataExport = 14,
            ErrNoColumnExport = 15,
            ObjectEmpty = 16,
            ErrorAuthorization = 17,

            KeyVaultSolicitudFallida = 96,
            KeyVaultFallaGuardar = 97,
            KeyVaultArgumentosNulos = 98,
            Unauthorized = 99,

            //// Comunes BD 10X - 100X
            ErrorGeneralDB = 100,

            ErrUniqueKey = 101,
            ErrRequiredField = 102,
            ErrForeingkey = 103,
            ErrMaxLength = 104,
            ErrMaxValue = 105,
            ExistField = 106,
            DatoRequerido = 107,
            Duplicado = 108,
            Rango = 109,
            Dependiente = 110,
            Longitud = 111,
            RangoFechasDesde = 112,
            RangoFechasHasta = 113,
            Formato = 114,
            MessageGeneral = 115,

            //// Validaciones Negocio 100X - 10000X
            SoloLectura = 1001,

            //// Error de Negocio 10000X:
        }
    }
}
