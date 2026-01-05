namespace Area52.Core.Domain;

/// <summary>
/// Domeinmodel voor een reservering in Area 52.
/// 
/// Bevat alle informatie die nodig is om te weten:
/// - wie wat wanneer boekt (Type, CheckIn, CheckOut, Persons),
/// - wat de prijs is (GrossAmount, DiscountAmount, NetAmount),
/// - in welke fase de boeking zit (Status).
/// 
/// Dit is het centrale resultaat van de reserveringsflow: de services vullen
/// dit model en de UI toont het aan de gebruiker.
/// </summary>

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