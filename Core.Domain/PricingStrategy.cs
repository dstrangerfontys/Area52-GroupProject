namespace Area52.Core.Domain;

public abstract class PricingStrategy
{
    public abstract decimal CalculateGross(QuoteRequest request, Rate rate);
}