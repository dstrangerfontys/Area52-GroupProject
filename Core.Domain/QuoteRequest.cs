namespace Area52.Core.Domain;

public class QuoteRequest
{
    public AccommodationType Type { get; set; }
    public DateOnly CheckIn { get; set; }
    public DateOnly CheckOut { get; set; }
    public int Persons { get; set; }  // alleen relevant voor camping
}