namespace Area52.Core.Domain
{
    /// <summary>
    /// Implementatie van IAvailabilityService.
    /// De logica is eenvoudig: we wijzen direct naar
    /// IAccommodationRepository.
    /// </summary>
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AvailabilityService(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        public IEnumerable<Accommodation> SearchAvailable(QuoteRequest request)
        {
            return _accommodationRepository.SearchAvailable(
                request.Type,
                request.CheckIn,
                request.CheckOut,
                request.Persons);
        }
    }
}