using System.Data;
using Area52.Core.Domain;
using MySql.Data.MySqlClient;

namespace Area52.Infrastructure.DataAccess;

public class MySqlRateRepository : IRateRepository
{
    private readonly string _connectionString;

    public MySqlRateRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Rate GetActiveRate(AccommodationType type)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Type, BaseNight, Cleaning, EnergyPerNight, PerPersonPerNight, WeekDiscount
            FROM Rates
            WHERE Type = @type
            LIMIT 1;
        ";
        command.Parameters.AddWithValue("@type", (int)type);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
        {
            throw new InvalidOperationException($"Geen tarief gevonden voor type {type}");
        }

        return new Rate
        {
            Id = reader.GetInt32("Id"),
            Type = (AccommodationType)reader.GetInt32("Type"),
            BaseNight = reader.GetDecimal("BaseNight"),
            Cleaning = reader.GetDecimal("Cleaning"),
            EnergyPerNight = reader.GetDecimal("EnergyPerNight"),
            PerPersonPerNight = reader.GetDecimal("PerPersonPerNight"),
            WeekDiscount = reader.GetDecimal("WeekDiscount")
        };
    }
}