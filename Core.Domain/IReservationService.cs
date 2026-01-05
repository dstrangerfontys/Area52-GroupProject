namespace Area52.Core.Domain;

public interface IReservationService
{
    Reservation CreateReservation(QuoteRequest request);
}