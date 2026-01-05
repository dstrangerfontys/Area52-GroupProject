namespace Area52.Core.Domain;

/// <summary>
/// Repository-interface voor het opvragen van tariefinformatie.
/// 
/// De domeinlaag weet alleen dat tarieven ergens vandaan komen, maar niet
/// of dat uit MySQL, een API of een in-memory lijst is. Door een interface
/// te gebruiken blijft de businesslogica onafhankelijk van de opslagtechniek.
/// </summary>

public interface IRateRepository
{
    Rate GetActiveRate(AccommodationType type);
}