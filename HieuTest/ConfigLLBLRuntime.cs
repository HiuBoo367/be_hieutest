using Npgsql;
using SD.LLBLGen.Pro.DQE.PostgreSql;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System.Diagnostics;

namespace App.Core.Common
{
    public static class ConfigLLBLRuntime
    {
        public static void ConfigLLBLRuntimeConfiguration()
        {
            RuntimeConfiguration.ConfigureDQE<PostgreSqlDQEConfiguration>(
                c => c.AddDbProviderFactory(typeof(NpgsqlFactory))
                       .SetTraceLevel(TraceLevel.Verbose));


            RuntimeConfiguration.Tracing
                     .SetTraceLevel("ORMPersistenceExecution", TraceLevel.Verbose)
                     .SetTraceLevel("ORMPlainSQLQueryExecution", TraceLevel.Verbose);

        }
    }
}
