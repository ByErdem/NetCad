using NetCad.Data.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NetCad.Data
{
    public class SqlDataContext : IDataContext, IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public SqlDataContext(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
        }

        public async Task OpenConnectionAsync()
        {
            try
            {
                await _connection.OpenAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task CloseConnectionAsync()
        {
            try
            {
                await Task.Run(() => _connection.Close());
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<SqlCommand> CreateCommandAsync(string query, CommandType commandType = CommandType.Text, SqlParameter[] parameters = null)
        {
            try
            {
                await OpenConnectionAsync();
                SqlCommand command = new SqlCommand(query, _connection)
                {
                    CommandType = commandType
                };

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string query, CommandType commandType = CommandType.Text, SqlParameter[] parameters = null)
        {
            try
            {
                var command = await CreateCommandAsync(query, commandType, parameters);
                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<object> ExecuteScalarAsync(string query, CommandType commandType = CommandType.Text, SqlParameter[] parameters = null)
        {
            try
            {
                var command = await CreateCommandAsync(query, commandType, parameters);
                return await command.ExecuteScalarAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DataTable> ExecuteQueryAsync(string query, CommandType commandType = CommandType.Text, SqlParameter[] parameters = null)
        {
            try
            {
                var command = await CreateCommandAsync(query, commandType, parameters);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    await Task.Run(() => adapter.Fill(dataTable));
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
