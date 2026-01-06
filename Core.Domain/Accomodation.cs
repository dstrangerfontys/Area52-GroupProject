namespace Area52.Core.Domain
{
    /// <summary>
    /// Model voor een concrete accommodatie in Area 52.
    /// Bijvoorbeeld: Bungalow 12, Chalet A4, Kampeerplaats 27.
    /// Deze klasse is nodig om bij een zoekopdracht alle beschikbare
    /// eenheden te kunnen tonen waar de klant uit kan kiezen.
    /// </summary>
    public class Accommodation
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public AccommodationType Type { get; set; }

        public int Capacity { get; set; }
    }
}