namespace Package.Utilities.Net.TextResponseApplication.Texts {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Transversal {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Transversal() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Package.Utilities.Net.TextResponseApplication.Texts.Transversal", typeof(Transversal).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_CON_00020&quot;,
        /// &quot;Title&quot;:&quot;¡Confirmación de modificación de información!&quot;,
        /// &quot;Message&quot;:&quot;¿Está seguro de llevar a cabo la modificación de la información registrada?&quot;, 
        /// &quot;MessageFormat&quot;:&quot;¿Está seguro de llevar a cabo la modificación de la información registrada?&quot;,
        /// &quot;Parameters&quot;:[],
        /// &quot;TypeMessage&quot;:&quot;Alert&quot;,
        /// &quot;TitleTag&quot;:&quot;Confirmación&quot; 
        ///}.
        /// </summary>
        internal static string Alert_Empresa_Confirmacion {
            get {
                return ResourceManager.GetString("Alert.Empresa.Confirmacion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se puede eliminar el registro porque tiene asociación con otros maestros..
        /// </summary>
        internal static string ErrForeingkey {
            get {
                return ResourceManager.GetString("ErrForeingkey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Existen datos demasiado extensos, por favor corrija el problema y vuelva a intentarlo..
        /// </summary>
        internal static string ErrMaxLength {
            get {
                return ResourceManager.GetString("ErrMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Algunos datos que han sido ingresados infringen restricciones de la base de datos, por favor corrija el problema y vuelva a intentarlo..
        /// </summary>
        internal static string ErrMaxValue {
            get {
                return ResourceManager.GetString("ErrMaxValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No hay columnas configuradas para generar el archivo de excel con la información..
        /// </summary>
        internal static string ErrNoColumnExport {
            get {
                return ResourceManager.GetString("ErrNoColumnExport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No hay datos para generar el archivo de excel con la información..
        /// </summary>
        internal static string ErrNoDataExport {
            get {
                return ResourceManager.GetString("ErrNoDataExport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El registro que intenta buscar no se encuentra..
        /// </summary>
        internal static string ErrNoEncontrado {
            get {
                return ResourceManager.GetString("ErrNoEncontrado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_ERR_00009&quot;,
        /// &quot;Title&quot;:&quot;¡Intento de vulneración a la seguridad!&quot;,
        /// &quot;Message&quot;:&quot;¡No estás autorizado para ejecutar la acción indicada. Serás direccionado de forma inmediata al inicio de sesión!&quot;,
        /// &quot;MessageFormat&quot;:&quot;¡No estás autorizado para ejecutar la acción indicada. Serás direccionado de forma inmediata al inicio de sesión!&quot;,
        /// &quot;Parameters&quot;:[],
        /// &quot;TypeMessage&quot;:&quot;Error&quot;,
        /// &quot;TitleTag&quot;:&quot;Autorizacion&quot; 
        ///}.
        /// </summary>
        internal static string Error_Empresa_Autorizacion {
            get {
                return ResourceManager.GetString("Error.Empresa.Autorizacion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_INF_00043&quot;,
        /// &quot;Title&quot;:&quot;¡Modificación de información de la empresa cancelada!&quot;,
        /// &quot;Message&quot;:&quot;¡Se ha cancelado la acción de modificar información para la empresa \&quot;{0}\&quot; , dado que no se ha encontrado ninguna diferencia con la información ya existente!&quot;, 
        /// &quot;MessageFormat&quot;:&quot;¡Se ha cancelado la acción de modificar información para la empresa \&quot;{0}\&quot; , dado que no se ha encontrado ninguna diferencia con la información ya existente!&quot;,
        /// &quot;Parameters&quot;:[&quot;$Nombre&quot;],
        /// &quot;TypeMessage&quot;:&quot;Error&quot;, [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string Error_Empresa_ModificarCancelar {
            get {
                return ResourceManager.GetString("Error.Empresa.ModificarCancelar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_ERR_00029&quot;,
        /// &quot;Title&quot;:&quot;¡Empresa no existente!&quot;,
        /// &quot;Message&quot;:&quot;¡La Empresa \&quot;{0}\&quot; que intentó modificar no existe!&quot;, 
        /// &quot;MessageFormat&quot;:&quot;¡La Empresa \&quot;{0}\&quot; que intentó modificar no existe!&quot;,
        /// &quot;Parameters&quot;:[&quot;$Nombre&quot;],
        /// &quot;TypeMessage&quot;:&quot;Error&quot;,
        /// &quot;TitleTag&quot;:&quot;existente&quot; 
        ///}.
        /// </summary>
        internal static string Error_Empresa_ModificarExistente {
            get {
                return ResourceManager.GetString("Error.Empresa.ModificarExistente", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Message&quot;:&quot;¡La empresa \&quot;{0}\&quot; no pudo ser registrada, debido a que ya existe. Puede mofificarla si así lo desea!&quot;,
        /// &quot;Parameters&quot;:[&quot;Nombre&quot;],
        /// &quot;MessageFormat&quot;:&quot;¡La empresa \&quot;{0}\&quot; no pudo ser registrada, debido a que ya existe. Puede mofificarla si así lo desea!&quot;
        ///}.
        /// </summary>
        internal static string Error_Empresa_MsjCreacion {
            get {
                return ResourceManager.GetString("Error.Empresa.MsjCreacion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_ERR_00006&quot;,
        /// &quot;Title&quot;:&quot;¡Problemas llevando a cabo la operación sobre \&quot;{0}\&quot; \&quot;{1}\&quot;: Rango dato no válido!&quot;,
        /// &quot;Message&quot;:&quot;¡Debes asegurarte que el rango del dato \&quot;{2}\&quot; sea válido. El rango válido es \&quot;{3}\&quot; !&quot;,
        /// &quot;MessageFormat&quot;:&quot;¡Debes asegurarte que el rango del dato \&quot;{2}\&quot; sea válido. El rango válido es \&quot;{3}\&quot; !&quot;,
        /// &quot;Parameters&quot;:[&quot;la&quot;,&quot;Empresa&quot;,&quot;$NombreDato&quot;,&quot;$rango&quot;],
        /// &quot;TypeMessage&quot;:&quot;Error&quot;,
        /// &quot;TitleTag&quot;:&quot;Rango&quot;
        ///}.
        /// </summary>
        internal static string Error_Empresa_Rango {
            get {
                return ResourceManager.GetString("Error.Empresa.Rango", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La validación de la {0} para la operación {1} y la {2} no estan implementadas.
        /// </summary>
        internal static string ErrorAuthorization {
            get {
                return ResourceManager.GetString("ErrorAuthorization", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ha ocurrido un error general en la aplicación. ID Trace: {0}.
        /// </summary>
        internal static string ErrorGeneral {
            get {
                return ResourceManager.GetString("ErrorGeneral", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ha ocurrido un error en la base de datos. ID Trace: {0}.
        /// </summary>
        internal static string ErrorGeneralDB {
            get {
                return ResourceManager.GetString("ErrorGeneralDB", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se ha especificado la cadena de conexión para acceder al blob storage para almacenar los Logs del sistema.
        /// </summary>
        internal static string ErrorStorageConnectionAzureStorage {
            get {
                return ResourceManager.GetString("ErrorStorageConnectionAzureStorage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se ha especificado la tabla para almacenar en el PartitionKey de los Logs del sistema.
        /// </summary>
        internal static string ErrorTableNameAzureStorage {
            get {
                return ResourceManager.GetString("ErrorTableNameAzureStorage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El registro no se puede eliminar porque tiene {0} registro(s) dependiente(s)..
        /// </summary>
        internal static string ErrRegistrosDependientes {
            get {
                return ResourceManager.GetString("ErrRegistrosDependientes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Algunos datos no han sido ingresados y son necesarios para completar la operación, por favor corrija el problema y vuelva a intentarlo..
        /// </summary>
        internal static string ErrRequiredField {
            get {
                return ResourceManager.GetString("ErrRequiredField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Esta intentando grabar valores duplicados en campos de valores únicos, por favor corrija el problema y vuelva a intentarlo..
        /// </summary>
        internal static string ErrUniqueKey {
            get {
                return ResourceManager.GetString("ErrUniqueKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Algunos datos que han sido ingresados  ya se encuentran registrados, por favor corrija el problema y vuelva a intentarlo..
        /// </summary>
        internal static string ExistField {
            get {
                return ResourceManager.GetString("ExistField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Lo valores para consultar el key vault estan vacios..
        /// </summary>
        internal static string KeyVaultArgumentosNulos {
            get {
                return ResourceManager.GetString("KeyVaultArgumentosNulos", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ocurrio un error al intertar almacenar el valor en el KeyVault.
        /// </summary>
        internal static string KeyVaultFallaGuardar {
            get {
                return ResourceManager.GetString("KeyVaultFallaGuardar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No fue posible conectarse con el KeyVault.
        /// </summary>
        internal static string KeyVaultSolicitudFallida {
            get {
                return ResourceManager.GetString("KeyVaultSolicitudFallida", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Evento general en la aplicación. ID Trace: {0}.
        /// </summary>
        internal static string MessageGeneral {
            get {
                return ResourceManager.GetString("MessageGeneral", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;mensaje&quot;:&quot;¡No está permitido modificar la información del dato \&quot;{0}\&quot; de un ejercicio presupuestal!&quot;,
        /// &quot;parametros&quot;:[&quot;Nombre&quot;],	
        /// &quot;mesajeConFormato&quot;:&quot;¡No está permitido modificar la información del dato \&quot;{0}\&quot; de un ejercicio presupuestal!&quot;,
        /// &quot;titulo&quot;:&quot;¡Problemas modificando la información del ejercicio presupuestal!&quot;
        ///}.
        /// </summary>
        internal static string Modificar_EjercicioPresupuestal_Error {
            get {
                return ResourceManager.GetString("Modificar.EjercicioPresupuestal.Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;mensaje&quot;:&quot;¡La empresa \&quot;{0}\&quot; ha sido inactivada de forma exitosa!&quot;,
        /// &quot;parametros&quot;:[&quot;Nombre&quot;],	
        /// &quot;mesajeConFormato&quot;:&quot;¡La empresa \&quot;{0}\&quot; ha sido inactivada de forma exitosa!&quot;,
        /// &quot;titulo&quot;:&quot;¡Empresa inactivada de forma exitosa!&quot;
        ///}.
        /// </summary>
        internal static string Modificar_Empresa_Estado_Confirmacion {
            get {
                return ResourceManager.GetString("Modificar.Empresa.Estado.Confirmacion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;mensaje&quot;:&quot;¡La empresa para la cual intentas cambiar el estado no existe!&quot;,
        /// &quot;parametros&quot;:[&quot;Nombre&quot;],	
        /// &quot;mesajeConFormato&quot;:&quot;¡La empresa para la cual intentas cambiar el estado no existe!&quot;,
        /// &quot;titulo&quot;:&quot;¡Problemas cambiando el estado de la empresa seleccionada!&quot;
        ///}.
        /// </summary>
        internal static string Modificar_Empresa_Estado_Error {
            get {
                return ResourceManager.GetString("Modificar.Empresa.Estado.Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;mensaje&quot;:&quot;¡El modelo \&quot;{0}\&quot; ha sido modificado de forma exitosa!&quot;,
        /// &quot;parametros&quot;:[&quot;Nombre&quot;],	
        /// &quot;mesajeConFormato&quot;:&quot;¡El modelo \&quot;{0}\&quot; ha sido modificado de forma exitosa!&quot;,
        /// &quot;titulo&quot;:&quot;¡Modelo modificado de forma exitosa!&quot;
        ///}.
        /// </summary>
        internal static string Modificar_Modelo_Confirmacion {
            get {
                return ResourceManager.GetString("Modificar.Modelo.Confirmacion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;mensaje&quot;:&quot;¡La información relacionada al modelo \&quot;{0}\&quot; no se ha modificado debido a que esa información ya existe!&quot;,
        /// &quot;parametros&quot;:[&quot;Nombre&quot;],	
        /// &quot;mesajeConFormato&quot;:&quot;¡La información relacionada al modelo \&quot;{0}\&quot; no se ha modificado debido a que esa información ya existe!&quot;,
        /// &quot;titulo&quot;:&quot;¡La información que desea registrar ya existe!&quot;
        ///}.
        /// </summary>
        internal static string Modificar_Modelo_Error {
            get {
                return ResourceManager.GetString("Modificar.Modelo.Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El registro fue modificado exitosamente..
        /// </summary>
        internal static string MsjActualizacionMessage {
            get {
                return ResourceManager.GetString("MsjActualizacionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ocurrio un error al intentar modificar el registro..
        /// </summary>
        internal static string MsjActualizacionMessageNegative {
            get {
                return ResourceManager.GetString("MsjActualizacionMessageNegative", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Modificación de Registro.
        /// </summary>
        internal static string MsjActualizacionTitle {
            get {
                return ResourceManager.GetString("MsjActualizacionTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El registro fue creado exitosamente..
        /// </summary>
        internal static string MsjCreacionMessage {
            get {
                return ResourceManager.GetString("MsjCreacionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ocurrio un error al intentar crear el registro..
        /// </summary>
        internal static string MsjCreacionMessageNegative {
            get {
                return ResourceManager.GetString("MsjCreacionMessageNegative", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Creación de Registro.
        /// </summary>
        internal static string MsjCreacionTitle {
            get {
                return ResourceManager.GetString("MsjCreacionTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El registro fue eliminado exitosamente..
        /// </summary>
        internal static string MsjEliminacionMessage {
            get {
                return ResourceManager.GetString("MsjEliminacionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ocurrio un error al intentar eliminar el registro..
        /// </summary>
        internal static string MsjEliminacionMessageNegative {
            get {
                return ResourceManager.GetString("MsjEliminacionMessageNegative", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Eliminacion de Registro.
        /// </summary>
        internal static string MsjEliminacionTitle {
            get {
                return ResourceManager.GetString("MsjEliminacionTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El objeto se encuentra vacio, por lo anterior no se puede procesar la información..
        /// </summary>
        internal static string ObjectEmpty {
            get {
                return ResourceManager.GetString("ObjectEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Registro antiguo vacío.
        /// </summary>
        internal static string RegistroAntiguoVacio {
            get {
                return ResourceManager.GetString("RegistroAntiguoVacio", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Registro nuevo y antiguo vacío.
        /// </summary>
        internal static string RegistroNuevoAntiguoVacio {
            get {
                return ResourceManager.GetString("RegistroNuevoAntiguoVacio", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Registro nuevo vacío.
        /// </summary>
        internal static string RegistroNuevoVacio {
            get {
                return ResourceManager.GetString("RegistroNuevoVacio", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_EXI_00010&quot;,
        /// &quot;Title&quot;:&quot;¡Empresa modificada de forma exitosa!&quot;,
        /// &quot;Message&quot;:&quot;¡La empresa \&quot;{0}\&quot; ha sido modificada de forma exitosa!&quot;,
        /// &quot;MessageFormat&quot;:&quot;¡La empresa \&quot;{0}\&quot; ha sido modificada de forma exitosa!&quot;,
        /// &quot;Parameters&quot;:[&quot;$Nombre&quot;],
        /// &quot;TypeMessage&quot;:&quot;Exito&quot;,
        /// &quot;TitleTag&quot;:&quot;Registro&quot; 
        ///}.
        /// </summary>
        internal static string Success_Empresa_MsjActualizacion {
            get {
                return ResourceManager.GetString("Success.Empresa.MsjActualizacion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_EXI_00001&quot;,
        /// &quot;Title&quot;:&quot;¡Empresa registrada de forma exitosa!&quot;,
        /// &quot;Message&quot;:&quot;¡La empresa \&quot;{0}\&quot; ha sido registrada de forma exitosa!&quot;,
        /// &quot;MessageFormat&quot;:&quot;¡La empresa \&quot;{0}\&quot; ha sido registrada de forma exitosa!&quot;,
        /// &quot;Parameters&quot;:[&quot;$Nombre&quot;],
        /// &quot;TypeMessage&quot;:&quot;Exito&quot;,
        /// &quot;TitleTag&quot;:&quot;Registro&quot; 
        ///}.
        /// </summary>
        internal static string Success_Empresa_MsjCreacion {
            get {
                return ResourceManager.GetString("Success.Empresa.MsjCreacion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El usuario {1} con cuenta de dominio {0}, no esta autorizado para ejecutar la acción solicitada..
        /// </summary>
        internal static string Unauthorized {
            get {
                return ResourceManager.GetString("Unauthorized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_ERR_00003&quot;,
        /// &quot;Title&quot;:&quot;¡Problemas llevando a cabo la operación sobre \&quot;{0}\&quot; \&quot;{1}\&quot;: Dato requerido no diligenciado!&quot;,
        /// &quot;Message&quot;:&quot;¡Debes registrar un valor para el dato \&quot;{2}\&quot;, el cual es obligatorio!&quot;, 
        /// &quot;MessageFormat&quot;:&quot;¡Debes registrar un valor para el dato \&quot;{2}\&quot;, el cual es obligatorio!&quot;,
        /// &quot;Parameters&quot;:[&quot;la&quot;,&quot;Empresa&quot;],
        /// &quot;TypeMessage&quot;:&quot;Error&quot;,
        /// &quot;TitleTag&quot;:&quot;Requerido&quot; 
        ///}.
        /// </summary>
        internal static string Warning_Empresa_DatoRequerido {
            get {
                return ResourceManager.GetString("Warning.Empresa.DatoRequerido", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_EXI_00002&quot;,
        /// &quot;Title&quot;:&quot;¡Empresa ya existente!&quot;,
        /// &quot;Message&quot;:&quot;¡La empresa \&quot;{0}\&quot; no pudo ser registrada, debido a que ya existe. Puede mofificarla si así lo desea!&quot;,
        /// &quot;MessageFormat&quot;:&quot;¡La empresa \&quot;{0}\&quot; no pudo ser registrada, debido a que ya existe. Puede mofificarla si así lo desea!&quot;,
        /// &quot;Parameters&quot;:[&quot;$Nombre&quot;], 
        /// &quot;TypeMessage&quot;:&quot;Error&quot;,
        /// &quot;TitleTag&quot;:&quot;Duplicado&quot;  
        ///}.
        /// </summary>
        internal static string Warning_Empresa_Duplicado {
            get {
                return ResourceManager.GetString("Warning.Empresa.Duplicado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_ERR_00004&quot;,
        /// &quot;Title&quot;:&quot;¡Problemas llevando a cabo la operación sobre \&quot;{0}\&quot; \&quot;{1}\&quot;: Formato dato no válido!&quot;,
        /// &quot;Message&quot;:&quot;¡Debes registrar un valor con formato válido para el dato \&quot;{2}\&quot;. El formato válido es \&quot;{3}\&quot;!&quot;,
        /// &quot;MessageFormat&quot;:&quot;¡Debes registrar un valor con formato válido para el dato \&quot;{2}\&quot;. El formato válido es \&quot;{3}\&quot;!&quot;,
        /// &quot;Parameters&quot;:[&quot;la&quot;,&quot;Empresa&quot;,&quot;$NombreDato&quot;,&quot;$Formato&quot;],
        /// &quot;TypeMessage&quot;:&quot;Error&quot;,
        /// &quot;TitleTag&quot;:&quot;Formato&quot;
        ///}.
        /// </summary>
        internal static string Warning_Empresa_Formato {
            get {
                return ResourceManager.GetString("Warning.Empresa.Formato", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_ERR_00005&quot;,
        /// &quot;Title&quot;:&quot;¡Problemas llevando a cabo la operación sobre \&quot;{0}\&quot; \&quot;{1}\&quot;: Longitud dato no válida!&quot;,
        /// &quot;Message&quot;:&quot;¡Debes asegurarte que la longitud del dato \&quot;{2}\&quot; sea válida. La longitud válida es \&quot;{3}\&quot;!&quot;,
        /// &quot;MessageFormat&quot;:&quot;¡Debes asegurarte que la longitud del dato \&quot;{2}\&quot; sea válida. La longitud válida es \&quot;{3}\&quot;!&quot;,
        /// &quot;Parameters&quot;:[&quot;la&quot;,&quot;Empresa&quot;], 
        /// &quot;TypeMessage&quot;:&quot;Error&quot;,
        /// &quot;TitleTag&quot;:&quot;Longitud&quot; 
        ///}.
        /// </summary>
        internal static string Warning_Empresa_Longitud {
            get {
                return ResourceManager.GetString("Warning.Empresa.Longitud", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_ERR_00029&quot;,
        /// &quot;Title&quot;:&quot;¡Empresa no existente!&quot;,
        /// &quot;Message&quot;:&quot;¡La Empresa \&quot;{0}\&quot; que intentó modificar no existe!&quot;,
        /// &quot;MessageFormat&quot;:&quot;¡La Empresa \&quot;{0}\&quot; que intentó modificar no existe!&quot;,
        /// &quot;Parameters&quot;:[&quot;$Nombre&quot;],
        /// &quot;TypeMessage&quot;:&quot;Warning&quot;,
        /// &quot;TitleTag&quot;:&quot;Registro&quot; 
        ///}.
        /// </summary>
        internal static string Warning_Empresa_MsjActualizacion {
            get {
                return ResourceManager.GetString("Warning.Empresa.MsjActualizacion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_ERR_00012&quot;,
        /// &quot;Title&quot;:&quot;¡Problemas llevando a cabo la operación sobre \&quot;{0}\&quot; \&quot;{1}\&quot;: Fechas no válidas!&quot;,
        /// &quot;Message&quot;:&quot;¡Debes asegurarte que la fecha hasta, sea mayor o igual a la fecha desde!&quot;,
        /// &quot;MessageFormat&quot;:&quot;¡Debes asegurarte que la fecha hasta, sea mayor o igual a la fecha desde!&quot;,
        /// &quot;Parameters&quot;:[&quot;la&quot;,&quot;Empresa&quot;],
        /// &quot;TypeMessage&quot;:&quot;Error&quot;,
        /// &quot;TitleTag&quot;:&quot;Rango&quot; 
        ///}.
        /// </summary>
        internal static string Warning_Empresa_RangoFechaHasta {
            get {
                return ResourceManager.GetString("Warning.Empresa.RangoFechaHasta", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a {
        /// &quot;Code&quot;:&quot;SEGURIDAD_USU_ERR_00007&quot;,
        /// &quot;Title&quot;:&quot;¡Problemas llevando a cabo la operación sobre \&quot;{0}\&quot; \&quot;{1}\&quot;: Fechas no válidas!&quot;,
        /// &quot;Message&quot;:&quot;¡Debes asegurarte que la fecha desde, sea menor o igual a la fecha hasta!&quot;,
        /// &quot;MessageFormat&quot;:&quot;¡Debes asegurarte que la fecha desde, sea menor o igual a la fecha hasta!&quot;,
        /// &quot;Parameters&quot;:[&quot;la&quot;,&quot;Empresa&quot;],
        /// &quot;TypeMessage&quot;:&quot;Error&quot;,
        /// &quot;TitleTag&quot;:&quot;Rango&quot;
        ///}.
        /// </summary>
        internal static string Warning_Empresa_RangoFechasDesde {
            get {
                return ResourceManager.GetString("Warning.Empresa.RangoFechasDesde", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a solo lectura.
        /// </summary>
        internal static string SoloLectura
        {
            get
            {
                return ResourceManager.GetString("SoloLectura", resourceCulture);
            }
        }
    }
}
