// <copyright file="EnumerationExtension.cs" company="Your Company">
// Copyright(c) 2020 All Rights Reserved Your Company.
// Proyecto: Framework Base Your Company
// </copyright>
// <summary>Enumeraciones de Configuración de Mensajes de la aplicación</summary>
namespace Package.Utilities.Net
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Se encarga de realizar operaciones de los enumerados.
    /// </summary>
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Metodo generico que se encarga de obtener la descripción de los enumerados.
        /// </summary>
        /// <typeparam name="T">Enumerado</typeparam>
        /// <returns>Descripción de los enumerados.</returns>
        public static IEnumerable<string> GetEnumerationsDescription<T>() where T : Enum
        {
            return from enumeration in Enum.GetValues(typeof(T)).Cast<T>()
                   select enumeration.GetEnumDescription();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            if (value != null)
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());

                if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
                {
                    return attributes.First().Description;
                }

                return value.ToString();
            }
            return null;
        }

    }
}
