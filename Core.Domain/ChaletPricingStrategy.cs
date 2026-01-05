namespace Area52.Core.Domain;

/// <summary>
/// Prijsstrategie voor chalets:
///   brutoprijs = nachten * (BaseNight + EnergyPerNight).
/// </summary>

public sealed class ChaletPricingStrategy : PricingStrategy
{
    public override decimal CalculateGross(QuoteRequest request, Rate rate)
    {
        int nights = (request.CheckOut.ToDateTime(TimeOnly.MinValue)
                    - request.CheckIn.ToDateTime(TimeOnly.MinValue)).Days;

        return nights * (rate.BaseNight + rate.EnergyPerNight);
    }
}