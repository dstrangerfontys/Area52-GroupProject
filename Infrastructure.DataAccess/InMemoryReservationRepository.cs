using Area52.Core.Domain;

namespace Area52.Infrastructure.DataAccess;

public class InMemoryReservationRepository : IReservationRepository
{
    private readonly List<Reservation> _reservations = new();
    private int _nextId = 1;

    public Reservation Add(Reservation reservation)
    {
        reservation.Id = _nextId++;
        _reservations.Add(reservation);
        return reservation;
    }

    public IEnumerable<Reservation> GetAll()
    {
        return _reservations;
    }
}