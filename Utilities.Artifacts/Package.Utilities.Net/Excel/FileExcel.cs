// <copyright file="Excel.cs" company="Your Company">
// Copyright(c) 2020 All Rights Reserved Your Company.
// Proyecto: Framework Base Your Company
// </copyright>
namespace Package.Utilities.Net.Excel
{
    using System.IO;

    /// <summary>
    /// Se encarga de administrar el archivo de excel para su descarga.
    /// </summary>
    public class FileExcel
    {
        /// <summary>
        /// Obtiene o establece el nombre del archivo.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Obtiene o establece el flujo del archivo de excel para su descarga.
        /// </summary>
        public MemoryStream FileStream { get; set; }
    }
}
