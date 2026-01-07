namespace Area52.Core.Domain;

/// <summary>
/// Domeinservice voor het daadwerkelijk aanmaken van een reservering.
/// 
/// Combineert:
/// - prijsberekening (via IQuoteService),
/// - opslag van de reservering (via IReservationRepository).
/// 
/// De UI spreekt deze service aan wanneer een gebruiker een boeking bevestigt.
/// </summary>

public interface IReservationService
{
    Reservation CreateReservation(QuoteRequest request, int accommodationId, int? customerId);
}