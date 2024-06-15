using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NetCad.Data.Interfaces
{
    public interface IDataContext
    {
        Task OpenConnectionAsync();
        Task CloseConnectionAsync();
        Task<SqlCommand> CreateCommandAsync(string query, CommandType commandType = CommandType.Text, SqlParameter[] parameters = null);
        Task<int> ExecuteNonQueryAsync(string query, CommandType commandType = CommandType.Text, SqlParameter[] parameters = null);
        Task<object> ExecuteScalarAsync(string query, CommandType commandType = CommandType.Text, SqlParameter[] parameters = null);
        Task<DataTable> ExecuteQueryAsync(string query, CommandType commandType = CommandType.Text, SqlParameter[] parameters = null);
    }
}
