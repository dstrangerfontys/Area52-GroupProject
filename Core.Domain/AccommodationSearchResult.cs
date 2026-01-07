namespace Area52.Core.Domain
{
    /// <summary>
    /// Resultaat van een beschikbaarheids/zoekactie.
    /// Dit object wordt gebruikt om UI te voeden met informatie over
    /// welke accommodaties beschikbaar zijn.
    /// </summary>
    public class AccommodationSearchResult
    {
        public int AccommodationId { get; set; }

        public string Name { get; set; } = string.Empty;

        public AccommodationType Type { get; set; }
        public int Capacity { get; set; }
        public decimal? NetPrice { get; set; }
    }
}