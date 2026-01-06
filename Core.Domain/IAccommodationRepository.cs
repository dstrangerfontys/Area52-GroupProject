namespace Area52.Core.Domain
{
    /// <summary>
    /// Repository-interface voor accommodaties.
    ///
    /// De domeinlaag weet alleen dat accommodaties ergens vandaan komen
    /// maar niet hoe. De concrete implementatie komt in
    /// Infrastructure.DataAccess.
    /// </summary>
    public interface IAccommodationRepository
    {
        IEnumerable<Accommodation> SearchAvailable(
            AccommodationType type,
            DateOnly checkIn,
            DateOnly checkOut,
            int persons);

        Accommodation? GetById(int id);
    }
}