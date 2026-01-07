using System;
using Area52.Core.Domain;
using Xunit;

namespace Area52.Core.Tests
{
    /// <summary>
    /// Unit tests voor de prijsberekening van kampeerplaatsen (FR-01).
    /// Formule:
    /// bruto = aantalNachten * personen * perPersonPerNight
    /// </summary>
    public class CampsitePricingStrategyTests
    {
        [Fact]
        public void CalculateGross_FiveNightsFourPersons_CalculatesCorrectPrice()
        {
            // ARRANGE
            // Periode: 1 jan t/m 6 jan = 5 nachten
            var request = new QuoteRequest
            {
                Type = AccommodationType.Campsite,
                CheckIn = new DateOnly(2025, 1, 1),
                CheckOut = new DateOnly(2025, 1, 6),
                Persons = 4
            };

            // Tarief: €10 per persoon per nacht
            var rate = new Rate
            {
                PerPersonPerNight = 10m
            };

            var strategy = new CampsitePricingStrategy();

            // ACT
            var gross = strategy.CalculateGross(request, rate);

            // ASSERT
            // nachten = 5
            // bruto = 5 * 4 * 10 = 200
            Assert.Equal(200m, gross);
        }

        [Fact]
        public void CalculateGross_CheckOutBeforeCheckIn_ThrowsArgumentException()
        {
            // ARRANGE: foutieve periode
            var request = new QuoteRequest
            {
                Type = AccommodationType.Campsite,
                CheckIn = new DateOnly(2025, 1, 10),
                CheckOut = new DateOnly(2025, 1, 9),
                Persons = 2
            };

            var rate = new Rate
            {
                PerPersonPerNight = 10m
            };

            var strategy = new CampsitePricingStrategy();

            Assert.Throws<ArgumentException>(() =>
            {
                strategy.CalculateGross(request, rate);
            });
        }
    }
}