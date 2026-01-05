namespace Area52.Core.Domain;

/// <summary>
/// Prijsstrategie voor bungalows, implementatie van IPricingStrategy.
/// 
/// Past de regels uit FR-01 toe voor bungalows:
///   brutoprijs = aantal nachten * BaseNight + Cleaning.
/// 
/// Naam: 'BungalowPricingStrategy' omdat dit de concrete strategie is voor
/// de prijsberekening van bungalow-reserveringen binnen het Strategy patroon.
/// </summary>

public sealed class BungalowPricingStrategy : PricingStrategy
{
    public override decimal CalculateGross(QuoteRequest request, Rate rate)
    {
        int nights = (request.CheckOut.ToDateTime(TimeOnly.MinValue)
                    - request.CheckIn.ToDateTime(TimeOnly.MinValue)).Days;

        return nights * rate.BaseNight + rate.Cleaning;
    }
}