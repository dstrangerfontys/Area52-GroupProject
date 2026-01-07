namespace Area52.Core.Domain;

/// <summary>
/// Domeinservice voor het aanmaken van een reservering.
///
/// Deze service combineert twee verantwoordelijkheden:
/// 1) Via IQuoteService de prijs laten berekenen (FR-01 & FR-02).
/// 2) Via IReservationRepository de reservering opslaan.
///
/// De UI (bijvoorbeeld de Razor Page IndexModel) roept alleen deze service aan
/// en spreekt niet direct met de repositories. Dat zorgt ervoor dat:
/// - alle business rules via de domeinlaag lopen,
/// - de WebApp niet weet hoe de database eruit ziet (losse koppeling),
/// - de logica goed testbaar blijft.
/// </summary>

public class ReservationService : IReservationService
{
    private readonly IQuoteService _quoteService;
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IQuoteService quoteService, IReservationRepository reservationRepository)
    {
        _quoteService = quoteService;
        _reservationRepository = reservationRepository;
    }
    public Reservation CreateReservation(QuoteRequest request, int accommodationId, int? customerId)
    {
        var quote = _quoteService.CalculateQuote(request);

        var reservation = new Reservation
        {
            AccommodationId = accommodationId,
            CustomerId = customerId,

            Type = request.Type,
            CheckIn = request.CheckIn,
            CheckOut = request.CheckOut,
            Persons = request.Persons,

            GrossAmount = quote.GrossAmount,
            DiscountAmount = quote.DiscountAmount,
            NetAmount = quote.NetAmount,

            Status = ReservationStatus.Planned,
            CreatedAt = DateTime.UtcNow
        };
        return _reservationRepository.Add(reservation);
    }
}