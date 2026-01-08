namespace Area52.Core.Domain;

/// <summary>
/// Repository-interface voor klanten (login / registreren).
/// </summary>
public interface ICustomerRepository
{
    Customer? GetById(int id);
    Customer? GetByEmail(string email);
    Customer Add(Customer customer);
}