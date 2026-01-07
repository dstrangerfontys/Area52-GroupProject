namespace Area52.Core.Domain
{
    /// <summary>
    /// Service die verantwoordelijk is voor het zoeken naar
    /// beschikbare accommodaties op basis van een QuoteRequest.
    /// 
    /// Hierdoor hoeft de UI niet direct met repositories te praten.
    /// </summary>
    public interface IAvailabilityService
    {
        IEnumerable<Accommodation> SearchAvailable(QuoteRequest request);
    }
}