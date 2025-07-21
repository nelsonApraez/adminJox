namespace Package.Utilities.Net.Exceptions
{
    using System;
    using System.Data.SqlClient;
    using System.Globalization;

    /// <summary>
    /// Helper Para Procesar las Excepciones
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleException(Exception ex)
        {
            var cusEnumMessage = EnumerationException.Message.ErrorGeneral;
            if (ex.GetType().FullName.Contains("SqlException"))
            {
                cusEnumMessage = GetCusEnumMessageBd((SqlException)ex);
                if (cusEnumMessage != EnumerationException.Message.ErrorGeneralDB)
                {
                    throw new CustomException(EnumerationException.TypeCustomException.Validation,
                                              cusEnumMessage, ex);
                }
            }

            var guidTrace = Guid.NewGuid();
            throw new CustomException(cusEnumMessage,
                                      guidTrace,
                                      new[] { Convert.ToString(guidTrace, CultureInfo.CurrentCulture) },
                                      ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlEx"></param>
        /// <returns></returns>
        private static EnumerationException.Message GetCusEnumMessageBd(SqlException sqlEx)
        {
            if (sqlEx.Number == Constants.CodeSql.CodeUniquekey || sqlEx.Number == Constants.CodeSql.CodeDuplicateKeyRestriction)
            {
                return EnumerationException.Message.ErrUniqueKey;
            }

            if (sqlEx.Number == Constants.CodeSql.CodeInsertValuessNulll)
            {
                return EnumerationException.Message.ErrRequiredField;
            }
            if (sqlEx.Number == Constants.CodeSql.CodeConflictRestriccion)
            {
                return EnumerationException.Message.ErrForeingkey;
            }
            if (sqlEx.Number == Constants.CodeSql.CodeDataTruncated)
            {
                return EnumerationException.Message.ErrMaxLength;
            }

            if (sqlEx.Number == Constants.CodeSql.CodeViolationOfMaxValueConstraint)
            {
                return EnumerationException.Message.ErrMaxValue;
            }

            return EnumerationException.Message.ErrorGeneralDB;
        }
    }
}
