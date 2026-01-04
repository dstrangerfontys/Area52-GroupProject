namespace Area52.Core.Domain;

public class Rate
{
    public int Id { get; set; }
    public AccommodationType Type { get; set; }

    public decimal BaseNight { get; set; }          // basisprijs per nacht
    public decimal Cleaning { get; set; }           // schoonmaak (bungalow)
    public decimal EnergyPerNight { get; set; }     // energie (chalet)
    public decimal PerPersonPerNight { get; set; }  // p.p.p.n. (camping)
    public decimal WeekDiscount { get; set; }       // vaste weekkorting
}