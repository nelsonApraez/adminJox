namespace Package.Utilities.Net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Clase para obtener los typos de objetos de la solucion de acuerdo al Attribute Extend Custom
    /// </summary>
    public static class ExtensionReferencedAssemblies
    {

        /// <summary>
        /// Obtiene los tipos de objetos de acuerdo type of Attribute
        /// </summary>
        /// <param name="attributeType">Attribute Extend para determinar las propiesdades de inyeccion de dependencias base </param>
        /// <param name="interfaceType">Nombre de interface padre que implementa los repositorios de negocio</param>
        /// <returns>Lista de Clases que estan creadas con la notacion del tipo Attribute Extend</returns>
        public static Dictionary<Type, Type> GetTypeClassReference(Type attributeType, string interfaceType)
        {
            var asmt = Assembly.GetEntryAssembly();
            if (asmt == null)
            {
                asmt = Assembly.GetExecutingAssembly();
            }
            var listreference = GetListOfEntryAssemblyWithReferences(asmt, asmt.FullName.Split('.')[0]);
            var listinterface = (from asm in listreference
                                 from typeinterface in asm.GetTypes()
                                 where typeinterface.IsInterface && typeinterface.GetInterfaces().Any(a => a.Name.Contains(interfaceType))
                                 select typeinterface).ToList();

            return (from asm in listreference
                    from type in asm.GetExportedTypes()
                    from typeinterface in listinterface
                    from propsm in type.GetCustomAttributes(attributeType)
                    where type.IsClass && type.IsPublic
                    where type.GetInterfaces().Any(a => a.Name.Contains(typeinterface.Name))
                    where typeinterface.Name.Contains(type.Name)
                    select new { Key = typeinterface, Value = type }).ToDictionary(pair => pair.Key, pair => pair.Value);

        }

        /// <summary>
        /// Este metodo retorna todos los Assembly que son dependiente del current Assembly
        /// </summary>
        /// <param name="mainAsm">Assembly refente o dependiente base</param>
        /// <param name="Filter">Name space de Assembly para evitar referencia todos los dll</param>
        /// <returns>Lista de Assembly Que tienen referecia al current Assembly</returns>
        public static List<Assembly> GetListOfEntryAssemblyWithReferences(Assembly mainAsm, string filter)
        {
            List<Assembly> listOfAssemblies = new List<Assembly>();
            if (mainAsm != null)
            {
                listOfAssemblies.Add(mainAsm);
                foreach (var refAsmName in mainAsm.GetReferencedAssemblies())
                {
                    if (refAsmName.FullName.Contains(filter))
                    {
                        var asse = Assembly.Load(refAsmName);
                        listOfAssemblies.Add(asse);
                        listOfAssemblies.AddRange(GetListOfEntryAssemblyWithReferences(asse, filter).Except(listOfAssemblies));
                    }
                }
            }
            return listOfAssemblies;
        }
    }
}
