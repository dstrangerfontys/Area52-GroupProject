using System;
using Area52.Core.Domain;
using Xunit;

namespace Area52.Core.Tests
{
    /// <summary>
    /// Unit tests voor de prijsberekening van bungalows (FR-01).
    /// Ik test hier specifiek:
    /// - nachten * basistarief + eenmalige schoonmaak
    /// </summary>
    public class BungalowPricingStrategyTests
    {
        [Fact]
        public void CalculateGross_ThreeNights_AddsCleaningOnce()
        {
            // ARRANGE 
            // Periode: 1 jan t/m 4 jan = 3 nachten
            var request = new QuoteRequest
            {
                Type = AccommodationType.Bungalow,
                CheckIn = new DateOnly(2025, 1, 1),
                CheckOut = new DateOnly(2025, 1, 4),
                Persons = 4 // voor bungalow maakt Persons niet uit
            };

            // Tarief: €100 per nacht + €50 schoonmaakkosten
            var rate = new Rate
            {
                BaseNight = 100m,
                Cleaning = 50m
            };

            var strategy = new BungalowPricingStrategy();

            // ACT (wanneer)
            var gross = strategy.CalculateGross(request, rate);

            // ASSERT (dan verwacht ik)
            // 3 nachten * 100 = 300
            // + 50 schoonmaak
            // = 350
            Assert.Equal(350m, gross);
        }

        [Fact]
        public void CalculateGross_InvalidDates_ThrowsException()
        {
            // ARRANGE: CheckOut vóór CheckIn → fout scenario
            var request = new QuoteRequest
            {
                Type = AccommodationType.Bungalow,
                CheckIn = new DateOnly(2025, 1, 10),
                CheckOut = new DateOnly(2025, 1, 9),
                Persons = 2
            };

            var rate = new Rate
            {
                BaseNight = 100m,
                Cleaning = 50m
            };

            var strategy = new BungalowPricingStrategy();

            // ACT + ASSERT: we verwachten een ArgumentException
            Assert.Throws<ArgumentException>(() =>
            {
                strategy.CalculateGross(request, rate);
            });
        }
    }
}