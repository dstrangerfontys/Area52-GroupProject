using Area52.Core.Domain;

namespace Area52.WebApp.Models
{
    /// <summary>
    /// ViewModel voor één zoekresultaat (één accommodatie) op de startpagina.
    /// Bevat alleen wat de UI nodig heeft om de kaart te tonen.
    /// </summary>
    public class AccommodationSearchResultViewModel
    {
        public int AccommodationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public AccommodationType Type { get; set; }
        public int Capacity { get; set; }
        public decimal? NetPrice { get; set; }
    }
}