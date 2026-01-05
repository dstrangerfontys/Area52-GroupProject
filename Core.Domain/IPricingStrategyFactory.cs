namespace Area52.Core.Domain;

/// <summary>
/// Interface voor een factory die de juiste IPricingStrategy oplevert
/// op basis van het gekozen accommodatietype.
/// 
/// Dit volgt het Factory design pattern: object-aanmaak wordt gecentraliseerd.
/// De rest van de applicatie hoeft niet te weten welke concrete klasse
/// gebruikt wordt, maar spreekt alleen met deze interface.
/// </summary>

public interface IPricingStrategyFactory
{
    PricingStrategy GetStrategy(AccommodationType type);
}