namespace Area52.Core.Domain;

public class ReservationService : IReservationService
{
    private readonly IQuoteService _quoteService;
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IQuoteService quoteService, IReservationRepository reservationRepository)
    {
        _quoteService = quoteService;
        _reservationRepository = reservationRepository;
    }

    public Reservation CreateReservation(QuoteRequest request)
    {
        // 1. Prijs berekenen (FR-01 + FR-02 via QuoteService)
        var quote = _quoteService.CalculateQuote(request);

        // 2. Reservering object vullen
        var reservation = new Reservation
        {
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

        // 3. Opslaan via repository
        return _reservationRepository.Add(reservation);
    }
}