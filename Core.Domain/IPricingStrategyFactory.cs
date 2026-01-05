namespace Area52.Core.Domain;

public interface IPricingStrategyFactory
{
    PricingStrategy GetStrategy(AccommodationType type);
}