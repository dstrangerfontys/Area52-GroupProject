namespace Area52.Core.Domain;

/// <summary>
/// Value object / DTO dat de invoer voor een prijsaanvraag of reservering bevat.
///
/// Dit object wordt:
/// - gevuld in de UI-laag (op basis van het formulier),
/// - doorgegeven aan de domeinservices (QuoteService, ReservationService),
/// - gebruikt door de prijsstrategieën (<see cref="PricingStrategy"/>).
///
/// Waarom een aparte QuoteRequest?
/// - De services hoeven niet direct aan een Razor Page of ViewModel gekoppeld te zijn.
/// - Zo blijft de domeinlaag onafhankelijk van de Web/UI-laag.
/// </summary>

public class QuoteRequest
{
    public AccommodationType Type { get; set; }
    public DateOnly CheckIn { get; set; }
    public DateOnly CheckOut { get; set; }
    public int Persons { get; set; }  // alleen relevant voor camping
}