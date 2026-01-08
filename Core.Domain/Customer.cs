namespace Area52.Core.Domain;

/// <summary>
/// Klant met login-gegevens.
/// </summary>
public class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string? Phone { get; set; }
    public string? Address { get; set; }
}