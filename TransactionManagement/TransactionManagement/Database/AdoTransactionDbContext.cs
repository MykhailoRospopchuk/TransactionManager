using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TransactionManagement.Model.Entities;
using TransactionManagement.Model.Enums;
using TransactionManagement.Model.RequestModel;

namespace TransactionManagement.Database
{
    public class AdoTransactionDbContext : DbContext
    {
        private readonly string _connectionStr;

        public AdoTransactionDbContext(DbContextOptions<AdoTransactionDbContext> options) : base(options)
        {
            _connectionStr = Database.GetConnectionString();
        }

        public async Task AddTransaction(IEnumerable<TransactRecord> income)
        {
            string sqlStatement = """
                MERGE INTO Transactions AS target
                USING (VALUES (@transactionId, @status, @type, @clientName, @amount)) AS source (TransactionId, Status, Type, ClientName, Amount)
                    ON target.TransactionId = source.TransactionId
                WHEN MATCHED THEN
                    UPDATE SET target.Status = @status
                WHEN NOT MATCHED THEN
                    INSERT (TransactionId, Status, Type, ClientName, Amount)
                    VALUES (source.TransactionId, source.Status, source.Type, source.ClientName, source.Amount);
                """;

            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.Add("@transactionId", SqlDbType.Int);
                    cmd.Parameters.Add("@status", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@type", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@clientName", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@amount", SqlDbType.Float);

                    await connection.OpenAsync();

                    foreach (var item in income)
                    {
                        cmd.Parameters["@transactionId"].Value = item.TransactionId;
                        cmd.Parameters["@status"].Value = item.Status;
                        cmd.Parameters["@type"].Value = item.Type;
                        cmd.Parameters["@clientName"].Value = item.ClientName;
                        cmd.Parameters["@amount"].Value = item.Amount;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public async Task<IEnumerable<TransactRecord>> GetAllTransactions()
        {
            List<TransactRecord> record = new List<TransactRecord>();

            string sqlStatement = """
                SELECT * FROM Transactions
                """;

            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlStatement, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    while (await reader.ReadAsync())
                    {
                        record.Add(ReaderToTransaction(reader));
                    }
                }
            }
            return record;
        }

        public async Task<IEnumerable<TransactRecord>> GetFilterTransactions(FilterTransactionCsvRequest filterRequest)
        {
            List<TransactRecord> record = new List<TransactRecord>();

            string sqlStatement = """
                SELECT * FROM Transactions
                """;

            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlStatement, connection);


                if (filterRequest.Type != null)
                {
                    command.CommandText += """
                         WHERE Type = @type
                        """;
                    command.Parameters.AddWithValue("@type", filterRequest.Type.ToString());

                    if (filterRequest.Status != null)
                    {
                        command.CommandText += """
                         AND Status = @status;
                        """;
                        command.Parameters.AddWithValue("@status", filterRequest.Status.ToString());
                    }
                }
                else if (filterRequest.Status != null)
                {
                    command.CommandText += """
                         WHERE Status = @status;
                        """;
                    command.Parameters.AddWithValue("@status", filterRequest.Status.ToString());
                }

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    while (await reader.ReadAsync())
                    {
                        record.Add(ReaderToTransaction(reader));
                    }
                }
            }
            return record;
        }


        public async Task<IEnumerable<TransactRecord>> GetFullFilterTransactions(FilterTransactionRequest filterRequest)
        {
            List<TransactRecord> record = new List<TransactRecord>();

            string sqlStatement = """
                SELECT * FROM Transactions
                """;

            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlStatement, connection);

                List<string> conditions = new List<string>();

                if (filterRequest.Type != null)
                {
                    conditions.Add("""
                        Type = @type
                        """);
                    command.Parameters.AddWithValue("@type", filterRequest.Type.ToString());
                }

                if (filterRequest.Status != null && filterRequest.Status.Any())
                {
                    string statusParameter = string.Join(", ", filterRequest.Status.Select(t => $"'{t.ToString()}'"));

                    conditions.Add($"Status IN ({statusParameter})");
                }

                if (!string.IsNullOrEmpty(filterRequest.ClientName))
                {
                    conditions.Add("""
                        ClientName LIKE @clientName
                        """);
                    command.Parameters.AddWithValue("@clientName", $"%{filterRequest.ClientName}%");
                }

                if (conditions.Count > 0)
                {
                    command.CommandText += $" WHERE {string.Join(" AND ", conditions)}";
                }

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    Console.WriteLine(command.CommandText);
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    while (await reader.ReadAsync())
                    {
                        record.Add(ReaderToTransaction(reader));
                    }
                }
            }
            return record;
        }

        private TransactRecord ReaderToTransaction(SqlDataReader reader)
        {
            return new TransactRecord()
            {
                TransactionId = Convert.ToInt32(reader["TransactionId"]),
                Status = Enum.Parse<StatusTransaction>(Convert.ToString(reader["Status"])),
                Type = Enum.Parse<TypeTransaction>(Convert.ToString(reader["Type"])),
                ClientName = Convert.ToString(reader["ClientName"]),
                Amount = Convert.ToDouble(reader["Amount"])
            };
        }
    }
}
