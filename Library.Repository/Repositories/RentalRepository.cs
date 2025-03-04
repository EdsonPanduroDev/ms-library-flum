using Dapper;
using Library.Domain.Aggregates.Rental;
using Library.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        readonly string _connectionString = string.Empty;
        public RentalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<int> Register(Rental rental)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var parameters = new DynamicParameters();
                        int result = 0;

                        parameters.Add("@poi_rental_id", rental.RentalId, DbType.Int32, ParameterDirection.InputOutput);
                        parameters.Add("@pii_user_id", rental.UserId, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@pii_copy_id", rental.CopyId, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@pii_registration_date", rental.RegisterDatetime, DbType.DateTime, ParameterDirection.Input);

                        await connection.ExecuteAsync(@"ADV_REGISTER_RENTAL", parameters, transaction, commandType: CommandType.StoredProcedure);

                        result = parameters.Get<int>("@poi_rental_id");

                        transaction.Commit();
                        return result;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new LibraryBaseException(e.Message);
                    }
                }
            }
        }
    }
}
