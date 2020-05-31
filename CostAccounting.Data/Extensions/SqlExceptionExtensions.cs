using CostAccounting.Core.Exceptions;
using Microsoft.Data.SqlClient;

namespace CostAccounting.Data.Extensions
{
    public static class SqlExceptionExtensions
    {
        public static RepositoryException ToRepositoryException(this SqlException sqlException)
        {
            var repositoryException = new RepositoryException(sqlException.Message, sqlException);
            foreach (SqlError error in sqlException.Errors)
            {
                var text = $"[#{error.Number}] Message: {error.Message}, " +
                           $"LineNumber: {error.LineNumber}, " +
                           $"Source: {error.Source}, " +
                           $"Procedure: {error.Procedure}";
                repositoryException.Errors.Add(text);
            }

            return repositoryException;
        }
    }
}