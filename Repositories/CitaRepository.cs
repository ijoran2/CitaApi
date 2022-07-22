using CitaAPI.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace CitaAPI.Repositories
{
    public class CitaRepository : ICitasRepository
    {
        private readonly string _connectionString;

        public CitaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<Citas>> Get()
        {
            var sqlQuery = "SELECT * FROM Citas";
                        
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Citas>(sqlQuery);
            }           
        }

        public async Task<Citas> Get(int Id)
        {
            var sqlQuery = "SELECT * FROM Citas WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Citas>(sqlQuery, new {Id = Id });
            }
        }

        public async Task<Citas> Create(Citas cita)
        {
            var sqlQuery = "INSERT INTO Citas (Fecha, Estado) VALUES (@Fecha, @Estado)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new 
                {
                    cita.Fecha,
                    cita.Estado,

                });

                return cita;
            }
        }

        public async Task Update(Citas cita)
        {
            var sqlQuery = "UPDATE Citas SET Fecha=@Fecha, Estado=@Estado WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    cita.Id,
                    cita.Fecha,
                    cita.Estado,
                });
            }
        }

        public async Task Delete(int Id)
        {
            var sqlQuery = "DELETE from Citas WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new { Id = Id });
            }
        }
    }
}
