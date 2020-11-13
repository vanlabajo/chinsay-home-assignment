using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoGreen.API.Queries
{
    public class VegetableQueries : IVegetableQueries
    {
        private readonly string connectionString = string.Empty;

        public VegetableQueries(IConfiguration configuration)
        {
            var constr = configuration.GetConnectionString("GoGreenContext");
            connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<VegetableDTO> GetVegetableAsync(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var result = await connection.QuerySingleOrDefaultAsync<VegetableDTO>(
                @"SELECT v.[Id], v.[Name], v.[Price], v.[RowVersion]
                    FROM [vegetables] v
                    WHERE v.Id = @id", new { id });

            if (result == null) throw new KeyNotFoundException();

            return result;
        }

        public async Task<IEnumerable<VegetableDTO>> GetVegetablesAsync()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            return await connection.QueryAsync<VegetableDTO>(
                @"SELECT v.[Id], v.[Name], v.[Price], v.[RowVersion]
                    FROM [vegetables] v");
        }
    }
}
