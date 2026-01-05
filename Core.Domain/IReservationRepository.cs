namespace Area52.Core.Domain;

public interface IReservationRepository
{
    Reservation Add(Reservation reservation);
    IEnumerable<Reservation> GetAll();
}