namespace Area52.Core.Domain;

/// <summary>
/// Domeinservice voor het berekenen van een offerte / prijsvoorstel.
/// 
/// Deze service:
/// - gebruikt tarieven (IRateRepository),
/// - kiest de juiste prijsstrategie (IPricingStrategyFactory),
/// - past kortingen toe (FR-02),
/// - geeft een gevuld Reservation-object terug met bruto/netto prijs.
/// 
/// Naam: 'QuoteService' omdat het gaat om het berekenen van een prijsvoorstel
/// op basis van invoer, los van daadwerkelijke opslag.
/// </summary>

public interface IQuoteService
{
    QuoteResult CalculateQuote(QuoteRequest request);
}