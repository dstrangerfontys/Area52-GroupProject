namespace Area52.Core.Domain;

public class QuoteResult
{
    public decimal GrossAmount { get; set; }     // brutoprijs
    public decimal DiscountAmount { get; set; }  // totale korting
    public decimal NetAmount { get; set; }       // netto prijs
}