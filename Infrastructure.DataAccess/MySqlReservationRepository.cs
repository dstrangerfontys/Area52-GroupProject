using Area52.Core.Domain;
using MySql.Data.MySqlClient;

namespace Area52.Infrastructure.DataAccess;

public class MySqlReservationRepository : IReservationRepository
{
    private readonly string _connectionString;

    public MySqlReservationRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Reservation Add(Reservation reservation)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Reservations
                (Type, CheckIn, CheckOut, Persons, GrossAmount, DiscountAmount, NetAmount, Status, CreatedAt, CustomerId)
            VALUES
                (@type, @checkIn, @checkOut, @persons, @gross, @discount, @net, @status, @createdAt, @customerId);
            SELECT LAST_INSERT_ID();
        ";

        command.Parameters.AddWithValue("@type", (int)reservation.Type);
        command.Parameters.AddWithValue("@checkIn", reservation.CheckIn.ToDateTime(TimeOnly.MinValue));
        command.Parameters.AddWithValue("@checkOut", reservation.CheckOut.ToDateTime(TimeOnly.MinValue));
        command.Parameters.AddWithValue("@persons", reservation.Persons);
        command.Parameters.AddWithValue("@gross", reservation.GrossAmount);
        command.Parameters.AddWithValue("@discount", reservation.DiscountAmount);
        command.Parameters.AddWithValue("@net", reservation.NetAmount);
        command.Parameters.AddWithValue("@status", (int)reservation.Status);
        command.Parameters.AddWithValue("@createdAt", reservation.CreatedAt);
        command.Parameters.AddWithValue("@customerId", DBNull.Value); // voor later

        var newId = Convert.ToInt32(command.ExecuteScalar());
        reservation.Id = newId;

        return reservation;
    }

    public IEnumerable<Reservation> GetAll()
    {
        var list = new List<Reservation>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Type, CheckIn, CheckOut, Persons, GrossAmount, DiscountAmount, NetAmount, Status, CreatedAt, CustomerId
            FROM Reservations;
        ";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var res = new Reservation
            {
                Id = reader.GetInt32("Id"),
                Type = (AccommodationType)reader.GetInt32("Type"),
                CheckIn = DateOnly.FromDateTime(reader.GetDateTime("CheckIn")),
                CheckOut = DateOnly.FromDateTime(reader.GetDateTime("CheckOut")),
                Persons = reader.GetInt32("Persons"),
                GrossAmount = reader.GetDecimal("GrossAmount"),
                DiscountAmount = reader.GetDecimal("DiscountAmount"),
                NetAmount = reader.GetDecimal("NetAmount"),
                Status = (ReservationStatus)reader.GetInt32("Status"),
                CreatedAt = reader.GetDateTime("CreatedAt")
                // CustomerId slaan we voor nu over
            };

            list.Add(res);
        }

        return list;
    }
}