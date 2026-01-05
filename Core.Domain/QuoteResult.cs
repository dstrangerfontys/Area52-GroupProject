namespace Area52.Core.Domain;

/// <summary>
/// Resultaat van een prijsberekening (offerte) voor een reservering.
///
/// Dit model bevat de volledige prijsopbouw:
/// - brutoprijs volgens de regels van FR-01,
/// - totale korting volgens FR-02,
/// - eindprijs (bruto - korting).
///
/// Waarom een aparte QuoteResult?
/// - De prijsberekening (offerte) kan los staan van het daadwerkelijk
///   opslaan van een reservering.
/// - Dezelfde logica kan gebruikt worden voor "prijs alleen berekenen"
///   én voor "prijs berekenen + reservering aanmaken".
/// </summary>

public class QuoteResult
{
    public decimal GrossAmount { get; set; }     // brutoprijs
    public decimal DiscountAmount { get; set; }  // totale korting
    public decimal NetAmount { get; set; }       // netto prijs
}