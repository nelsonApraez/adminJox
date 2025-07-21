using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;


namespace UnitTestsProject.Tests.UtilitiesTests
{
    public class SqlExceptionBuilder
    {
        private int errorNumber;
        private string errorMessage;

        public SqlException Build()
        {
            SqlError error = CreateError();
            SqlErrorCollection errorCollection = CreateErrorCollection(error);
            SqlException exception = CreateException(errorCollection);

            return exception;
        }

        public SqlExceptionBuilder WithErrorNumber(int number)
        {
            errorNumber = number;
            return this;
        }

        public SqlExceptionBuilder WithErrorMessage(string message)
        {
            errorMessage = message;
            return this;
        }

        private SqlError CreateError()
        {
            // Create instance via reflection...
            var ctors = typeof(SqlError).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var firstSqlErrorCtor = ctors.LastOrDefault();
            SqlError error = firstSqlErrorCtor.Invoke(
                new object[]
                {
                errorNumber,
                new byte(),
                new byte(),
                string.Empty,
                string.Empty,
                string.Empty,
                new int(),
                new Exception()
                }) as SqlError;

            return error;
        }

        private static SqlErrorCollection CreateErrorCollection(SqlError error)
        {
            // Create instance via reflection...
            var sqlErrorCollectionCtor = typeof(SqlErrorCollection).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            SqlErrorCollection errorCollection = sqlErrorCollectionCtor.Invoke(Array.Empty<object>()) as SqlErrorCollection;

            // Add error...
            typeof(SqlErrorCollection).GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(errorCollection, new object[] { error });

            return errorCollection;
        }

        private SqlException CreateException(SqlErrorCollection errorCollection)
        {
            // Create instance via reflection...
            var ctor = typeof(SqlException).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            SqlException sqlException = ctor.Invoke(
                new object[]
                { 
                // With message and error collection...
                errorMessage,
                errorCollection,
                null,
                Guid.NewGuid()
                }) as SqlException;

            return sqlException;
        }
    }
}
