namespace Area52.Core.Domain;

public interface IRateRepository
{
    Rate GetActiveRate(AccommodationType type);
}