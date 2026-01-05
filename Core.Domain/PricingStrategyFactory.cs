namespace Area52.Core.Domain;

/// <summary>
/// Concrete factory die op basis van AccommodationType de juiste
/// IPricingStrategy teruggeeft (Bungalow/Chalet/Campsite).
/// 
/// Voordeel:
/// - De logica om een concrete strategie te kiezen staat op één plek.
/// - Overige code werkt alleen met IPricingStrategy en blijft simpel.
/// - Nieuwe types (bijv. Villa) zijn eenvoudig toe te voegen zonder
///   bestaande services of UI aan te passen.
/// </summary>

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