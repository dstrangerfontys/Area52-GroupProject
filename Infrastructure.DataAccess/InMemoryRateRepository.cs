using Area52.Core.Domain;

namespace Area52.Infrastructure.DataAccess;

public class InMemoryRateRepository : IRateRepository
{
    private readonly List<Rate> _rates = new()
    {
        new Rate
        {
            Id = 1,
            Type = AccommodationType.Bungalow,
            BaseNight = 100m,
            Cleaning = 60m,
            EnergyPerNight = 0m,
            PerPersonPerNight = 0m,
            WeekDiscount = 50m
        },
        new Rate
        {
            Id = 2,
            Type = AccommodationType.Chalet,
            BaseNight = 90m,
            Cleaning = 0m,
            EnergyPerNight = 10m,
            PerPersonPerNight = 0m,
            WeekDiscount = 40m
        },
        new Rate
        {
            Id = 3,
            Type = AccommodationType.Campsite,
            BaseNight = 0m,
            Cleaning = 0m,
            EnergyPerNight = 0m,
            PerPersonPerNight = 8m,
            WeekDiscount = 0m
        }
    };

    public Rate GetActiveRate(AccommodationType type)
    {
        var rate = _rates.FirstOrDefault(r => r.Type == type);
        if (rate == null)
            throw new InvalidOperationException($"Geen tarief gevonden voor type {type}");

        return rate;
    }
}