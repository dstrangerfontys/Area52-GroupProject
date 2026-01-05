namespace Area52.Core.Domain;

public sealed class CampsitePricingStrategy : PricingStrategy
{
    public override decimal CalculateGross(QuoteRequest request, Rate rate)
    {
        int nights = (request.CheckOut.ToDateTime(TimeOnly.MinValue)
                    - request.CheckIn.ToDateTime(TimeOnly.MinValue)).Days;

        return nights * request.Persons * rate.PerPersonPerNight;
    }
}