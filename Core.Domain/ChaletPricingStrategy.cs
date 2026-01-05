namespace Area52.Core.Domain;

public sealed class ChaletPricingStrategy : PricingStrategy
{
    public override decimal CalculateGross(QuoteRequest request, Rate rate)
    {
        int nights = (request.CheckOut.ToDateTime(TimeOnly.MinValue)
                    - request.CheckIn.ToDateTime(TimeOnly.MinValue)).Days;

        return nights * (rate.BaseNight + rate.EnergyPerNight);
    }
}