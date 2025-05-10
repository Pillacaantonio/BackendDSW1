using Microsoft.Data.SqlClient;

namespace ProyectoApi.Helpers
{
    public interface IDatabaseExecutor
    {
        Task<TResult> ExecuteCommand<TResult>(Func<SqlConnection, Task<TResult>> operation);

    }
}
