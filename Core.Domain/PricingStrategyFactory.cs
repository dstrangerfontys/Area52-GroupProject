namespace Area52.Core.Domain;

public class PricingStrategyFactory : IPricingStrategyFactory
{
    public PricingStrategy GetStrategy(AccommodationType type)
    {
        return type switch
        {
            AccommodationType.Bungalow => new BungalowPricingStrategy(),
            AccommodationType.Chalet => new ChaletPricingStrategy(),
            AccommodationType.Campsite => new CampsitePricingStrategy(),
            _ => throw new NotSupportedException($"No pricing strategy for type {type}")
        };
    }
}