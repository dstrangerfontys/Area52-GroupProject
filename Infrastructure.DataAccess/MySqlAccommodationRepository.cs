using System.Data;
using Area52.Core.Domain;
using MySql.Data.MySqlClient;

namespace Area52.Infrastructure.DataAccess
{
    /// <summary>
    /// MySQL-implementatie van IAccommodationRepository.
    /// </summary>
    public class MySqlAccommodationRepository : IAccommodationRepository
    {
        private readonly string _connectionString;

        public MySqlAccommodationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Accommodation> SearchAvailable(
            AccommodationType type,
            DateOnly checkIn,
            DateOnly checkOut,
            int persons)
        {
            var results = new List<Accommodation>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var sql = @"
                SELECT Id, Code, Name, Type, Capacity
                FROM Accommodations
                WHERE Type = @Type
                  AND Capacity >= @Persons
                  AND IsActive = 1;";

            using var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Type", (int)type);
            cmd.Parameters.AddWithValue("@Persons", persons);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var acc = new Accommodation
                {
                    Id = reader.GetInt32("Id"),
                    Code = reader.GetString("Code"),
                    Name = reader.GetString("Name"),
                    Type = (AccommodationType)reader.GetInt32("Type"),
                    Capacity = reader.GetInt32("Capacity")
                };

                results.Add(acc);
            }

            return results;
        }

        public Accommodation? GetById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var sql = @"
                SELECT Id, Code, Name, Type, Capacity
                FROM Accommodations
                WHERE Id = @Id;";

            using var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return new Accommodation
            {
                Id = reader.GetInt32("Id"),
                Code = reader.GetString("Code"),
                Name = reader.GetString("Name"),
                Type = (AccommodationType)reader.GetInt32("Type"),
                Capacity = reader.GetInt32("Capacity")
            };
        }
    }
}