namespace Area52.Core.Domain;

public class Reservation
{
    public int Id { get; set; }

    public AccommodationType Type { get; set; }

    public DateOnly CheckIn { get; set; }
    public DateOnly CheckOut { get; set; }
    public int Persons { get; set; }

    public decimal GrossAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal NetAmount { get; set; }

    public ReservationStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}