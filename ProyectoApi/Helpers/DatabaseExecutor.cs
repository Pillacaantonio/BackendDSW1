using Microsoft.Data.SqlClient;

namespace ProyectoApi.Helpers
{
    public class DatabaseExecutor : IDatabaseExecutor
    {
        private readonly string _cadena;

        public DatabaseExecutor(string cadena)
        {
            _cadena = cadena;
        }
        public async Task<TResult> ExecuteCommand<TResult>(Func<SqlConnection, Task<TResult>> operation)
        {
            try
            {
                using var conexion = new SqlConnection(_cadena);
                return await operation(conexion);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}
