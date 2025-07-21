namespace Domain.Common.Enums
{

    public enum MessageEnums
    {

        EntityActivate,
        StateEqual,
        StateDifferent,
        Identificador,
        Creacion,
        ModeloConfig,
        ModeloConfigTodos,
        ListaVacia,
        ValorIgual,
        Eliminacion,

    }

    public static class EnumsEstado
    {
        public const string
        ACTIVO = "Activo",
        INACTIVO = "Inactivo",
        TRUE = "True",
        NOAPLICA = "No aplica",
        INACTIVOI = "I";
    }

    public static class EnumsMeses
    {
        public const string
        ENERO = "Enero",
        FEBRERO = "Febrero",
        MARZO = "Marzo",
        ABRIL = "Abril",
        MAYO = "Mayo",
        JUNIO = "Junio",
        JULIO = "Julio",
        AGOSTO = "Agosto",
        SEPTIEMBRE = "Septiembre",
        OCTUBRE = "Octubre",
        NOVIEMBRE = "Noviembre",
        DICIEMBRE = "Diciembre";
    }

    public enum Acciones
    {
        NA,
        Create,
        Read,
        Update,
        Delete
    }


    public static class EnumFormatosFecha
    {
        public const string
            FORMATO1 = "dd-MM-yy",
            FORMATO2 = "dd/MM/yy",
            FORMATO3 = "dd/mm/yy",
            FORMATO4 = "dd-mm-yy",
            FORMATO5 = "dd/MM/yyyy";
    }

    public static class EnumsSiNo
    {
        public const string
        SI = "SI",
        NO = "NO";

    }


    public static class EnumMensajesAplicacion
    {
        public const string
        LLAVE_NO_DEFINIDA = "Llave primaria no definida en el dominio de la entidad",
        BODY_EMPTY = "A non-empty request body is required.",
        ERROR_SERIALIZACION = "Invalid error serialization: ",
        FOR_ID = " for Id ",
        REGISTRO_NO_ENCONTRADO = "Record not found",
        COLLECCION_DEBE_CONTENER = "The collection must contain ",
        ITEM_DEBE_CONTENER = " items or more. It contains ",
        ERROR_MAPPER = "No se ha creado la configuración de mapper para el servicio",
        ITEMS = " items.";
    }

    public enum EnumsMes
    {
        Enero = 1,
        Febrero = 2,
        Marzo = 3,
        Abril = 4,
        Mayo = 5,
        Junio = 6,
        Julio = 7,
        Agosto = 8,
        Septiembre = 9,
        Octubre = 10,
        Noviembre = 11,
        Diciembre = 12
    }



    public enum Niveles
    {
        Nivel0 = 0,
        Nivel1 = 1,
        Nivel2 = 2,
        Nivel3 = 3,
        Nivel4 = 4,
        Nivel6 = 6
    }

    public static class FechaActivo
    {
        public const string
        DESDE = "ActivoDesde",
        HASTA = "ActivoHasta";
    }

    public static class FechaActivoConEspacio
    {
        public const string
        DESDE = "Activo Desde",
        HASTA = "Activo Hasta";
    }


    public static class ValueObjectEnum
    {
        public const string
        VALOR = "Valor",
        VALUE_OBJECT = "ValueObject";
    }


    public static class EstadoProcesoEnum
    {
        public const string
        NUEVO = "Nuevo",
        EJECUCION = "Ejecución",
        NOSELECCION = "No selección",
        CANCELADO = "Cancelado",
        TERMINADO = "Terminado",
        ERROR = "Error",
        EXITOSO = "Exitoso",
        FALLIDO = "Fallido",
        REINTENTO = "Reintento",
        ADVERTENCIA = "Advertencia";
    }


    public static class TipoConectorEnum
    {
        public const string
        APIPOST = "ApiPost",
        APIGET = "ApiGet",
        APIMANAGEMENTGET = "ApiManagementGet",
        APIMANAGEMENTPOST = "ApiManagementPost",
        STOREDPROCEDURE = "StoredProcedure",
        AZUREFUNCTION = "AzureFunction",
        LOGICAPP = "LogicApp",
        AZUREDATAFACTORY = "AzureDataFactory";

    }


}
