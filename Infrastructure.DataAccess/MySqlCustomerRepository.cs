using System.Data;
using Area52.Core.Domain;
using MySql.Data.MySqlClient;

namespace Area52.Infrastructure.DataAccess;

/// <summary>
/// MySQL-implementatie van ICustomerRepository.
/// </summary>
public class MySqlCustomerRepository : ICustomerRepository
{
    private readonly string _connectionString;

    public MySqlCustomerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Customer? GetById(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        const string sql = @"
            SELECT Id, Name, Email, PasswordHash, Phone, Address
            FROM Customers
            WHERE Id = @Id;";

        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return Map(reader);
    }

    public Customer? GetByEmail(string email)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        const string sql = @"
            SELECT Id, Name, Email, PasswordHash, Phone, Address
            FROM Customers
            WHERE Email = @Email;";

        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Email", email);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return Map(reader);
    }

    public Customer Add(Customer customer)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        const string sql = @"
            INSERT INTO Customers (Name, Email, PasswordHash, Phone, Address)
            VALUES (@Name, @Email, @PasswordHash, @Phone, @Address);
            SELECT LAST_INSERT_ID();";

        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Name", customer.Name);
        cmd.Parameters.AddWithValue("@Email", customer.Email);
        cmd.Parameters.AddWithValue("@PasswordHash", customer.PasswordHash);
        cmd.Parameters.AddWithValue("@Phone", (object?)customer.Phone ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Address", (object?)customer.Address ?? DBNull.Value);

        var idObj = cmd.ExecuteScalar();
        customer.Id = Convert.ToInt32(idObj);

        return customer;
    }

    private static Customer Map(IDataRecord reader)
    {
        return new Customer
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Email = reader.GetString(reader.GetOrdinal("Email")),
            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
            Phone = reader["Phone"] as string,
            Address = reader["Address"] as string
        };
    }
}