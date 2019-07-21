using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Common;

namespace AutoLotDAL.Interception
{
    class ConsoleWriterInterceptor : IDbCommandInterceptor
    {
        private void WriteInfo(bool isAsync, string commandText)
        {
            Console.WriteLine($"Is Async: {isAsync}, Command Text: {commandText}");
        }
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }
    }
}
