namespace Package.Utilities.Net.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using OfficeOpenXml.Table;

    /// <summary>
    /// Se encarga de administrar los componentes de excel.
    /// </summary>
    public static class Excel
    {
        /// <summary>
        /// Formato para las fechas.
        /// </summary>
        private static readonly string FormatDateTime = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Se encarga de descargar un archivo de excel.
        /// </summary>
        /// <param name="fnList">Function Delegada para obtener la Lista de Datos.</param>
        /// <param name="className">Contiene el nombre de la entidad a exportar.</param>
        /// <returns>Archivo de excel descargado.</returns>
        public static async Task<FileExcel> GetFileExcelAsync<T>(Func<Task<List<T>>> fnList, string className)
        {
            PropertyInfo[] membersToInclude = GetMemberToInclude<T>();
            if (membersToInclude == null || membersToInclude.Length == 0)
            {
                throw new CustomException(EnumerationException.TypeCustomException.NoContent, EnumerationException.Message.ErrNoColumnExport);
            }

            List<T> dataExcel = await fnList();
            if (dataExcel.IsNotNull())
            {
                return GenerateExcel(membersToInclude, dataExcel, className);
            }

            throw new CustomException(EnumerationException.TypeCustomException.NoContent, EnumerationException.Message.ErrNoDataExport);
        }

        /// <summary>
        /// Se encarga de descargar un archivo de excel.
        /// </summary>
        /// <param name="membersToInclude">Contiene la información de las propiedades.</param>
        /// <param name="lstDataExcel">Contiene la información a exportar.</param>
        /// <param name="className">Contiene el nombre de la entidad a exportar.</param>
        /// <returns>Archivo de excel descargado.</returns>
        private static FileExcel GenerateExcel<T>(PropertyInfo[] membersToInclude, List<T> lstDataExcel, string className)
        {
            MemoryStream stream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add($"Datos {className}");

                workSheet.Cells.LoadFromCollection(lstDataExcel, true, TableStyles.Light21,
                                                                    BindingFlags.Instance | BindingFlags.Public,
                                                                    membersToInclude);

                LoadGenericConfigurationHeader(workSheet, membersToInclude);

                EstablishGenericStyleExcelRange(membersToInclude, workSheet);

                package.Save();
            }

            stream.Position = 0;

            return new FileExcel
            {
                FileName = $"{className}-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx",
                FileStream = stream
            };
        }

        /// <summary>
        /// Se encarga de obtener la información de las propiedades genericas.
        /// </summary>
        /// <returns>Información de las propiedades.</returns>
        private static PropertyInfo[] GetMemberToInclude<T>()
        {
            return typeof(T)
                   .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                   .Where(property => !Attribute.IsDefined(property, typeof(Newtonsoft.Json.JsonIgnoreAttribute)) &&
                                      !(((ExcelPropertyAttribute)property.GetCustomAttribute(typeof(ExcelPropertyAttribute)))?.Hidden ?? false))
                   .OrderBy(property => ((ExcelPropertyAttribute)property.GetCustomAttribute(typeof(ExcelPropertyAttribute)))?.Order ?? property.GetHashCode())
                   .ToArray();
        }

        /// <summary>
        /// Configuracion de formatos y carga de nombres personalizados al header del excel
        /// </summary>
        /// <param name="workSheet">Hoja de excel</param>
        /// <param name="propertyInfos">propiedades cargadas de la entidad para la generacion del excel</param>        
        private static void LoadGenericConfigurationHeader(ExcelWorksheet workSheet, PropertyInfo[] propertyInfos)
        {
            for (int position = 0; position < propertyInfos.Length; position++)
            {
                var property = propertyInfos[position];

                var attribute = (ExcelPropertyAttribute)property.GetCustomAttribute(typeof(ExcelPropertyAttribute));

                if (attribute.IsNotNull())
                {
                    workSheet.Cells[1, position + 1].Value = attribute.Name;
                }

                if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    workSheet.Column(position + 1).Style.Numberformat.Format = FormatDateTime;
                }
            }
        }

        /// <summary>
        /// Se encarga de establecer los estilos de los rangos de las celdas de excel.
        /// </summary>
        /// <param name="membersToInclude">Contiene la información de las propiedades.</param>
        /// <param name="workSheet">Hoja de excel.</param>
        private static void EstablishGenericStyleExcelRange(PropertyInfo[] membersToInclude, ExcelWorksheet workSheet)
        {
            using ExcelRange excelRange = workSheet.Cells[1, 1, 1, membersToInclude.Length];
            excelRange.Style.Font.Bold = true;
            excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 121, 52));
            excelRange.Style.Font.Color.SetColor(Color.White);
        }
    }
}
