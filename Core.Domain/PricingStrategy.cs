namespace Area52.Core.Domain;

/// <summary>
/// Abstracte basis voor alle prijsstrategieën voor accommodaties.
///
/// Dit is de kern van het Strategy-pattern in dit project:
/// - Elke accommodatie (bungalow, chalet, kampeerplaats) krijgt
///   een eigen concrete klasse die van PricingStrategy
/// - Alle strategieën gebruiken dezelfde methode CalculateGross
///   maar de implementatie verschilt per type.
///
/// Waarom een abstracte class en geen interface?
/// - Als we later gedeelde hulpfuncties willen toevoegen
///   (bijvoorbeeld een methode om aantal nachten te berekenen),
///   kunnen we die hier implementeren en delen met alle strategies.
/// </summary>

public abstract class PricingStrategy
{
    public abstract decimal CalculateGross(QuoteRequest request, Rate rate);
}