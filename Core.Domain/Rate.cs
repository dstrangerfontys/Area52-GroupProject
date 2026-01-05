namespace Area52.Core.Domain;

/// <summary>
/// Domeinmodel voor tariefinformatie per accommodatietype.
/// 
/// Dit model hoort bij de prijsregels uit FR-01 en FR-02:
/// - BaseNight          : basistarief per nacht (bungalow/chalet)
/// - Cleaning           : eenmalige schoonmaakkosten (bungalow)
/// - EnergyPerNight     : energievergoeding per nacht (chalet)
/// - PerPersonPerNight  : prijs per persoon per nacht (kampeerplaats)
/// - WeekDiscount       : vaste weekkorting (bungalow/chalet, FR-02)
/// 
/// 'Rate' is bewust generiek gehouden zodat één type meerdere tariefversies
/// kan hebben (bijv. hoog-/laagseizoen) zonder dat de rest van de code hoeft
/// te veranderen.
/// </summary>

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