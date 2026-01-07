using System;
using Area52.Core.Domain;
using Xunit;

namespace Area52.Core.Tests
{
    /// <summary>
    /// Unit tests voor de gratis-nacht-korting op kampeerplaatsen (FR-02).
    /// Regel:
    /// - Bij aantalNachten &gt;= 4 krijgt de gast 1 nacht gratis
    ///   (prijs van 1 nacht * aantalPersonen).
    /// </summary>
    public class CampsiteFreeNightDiscountStrategyTests
    {
        [Fact]
        public void CalculateDiscount_ThreeNights_NoDiscount()
        {
            // ARRANGE
            var request = new QuoteRequest
            {
                Type = AccommodationType.Campsite,
                CheckIn = new DateOnly(2025, 5, 1),
                CheckOut = new DateOnly(2025, 5, 4), // 3 nachten
                Persons = 4
            };

            var rate = new Rate
            {
                PerPersonPerNight = 10m
            };

            var gross = 3 * 4 * 10m; // 120

            var strategy = new CampsiteFreeNightDiscountStrategy();

            // ACT
            var discount = strategy.CalculateDiscount(request, rate, gross);

            // ASSERT: bij < 4 nachten geen korting
            Assert.Equal(0m, discount);
        }

        [Fact]
        public void CalculateDiscount_FourNights_GetsOneNightFree()
        {
            // ARRANGE
            var request = new QuoteRequest
            {
                Type = AccommodationType.Campsite,
                CheckIn = new DateOnly(2025, 5, 1),
                CheckOut = new DateOnly(2025, 5, 5), // 4 nachten
                Persons = 2
            };

            var rate = new Rate
            {
                PerPersonPerNight = 10m
            };

            var nights = 4;
            var gross = nights * request.Persons * rate.PerPersonPerNight;

            var strategy = new CampsiteFreeNightDiscountStrategy();

            // ACT
            var discount = strategy.CalculateDiscount(request, rate, gross);

            // ASSERT
            // 1 nacht gratis = 1 * personen * pppn = 1 * 2 * 10 = 20
            Assert.Equal(20m, discount);
        }

        [Fact]
        public void CalculateDiscount_SevenNights_StillOnlyOneNightFree()
        {
            // ARRANGE
            var request = new QuoteRequest
            {
                Type = AccommodationType.Campsite,
                CheckIn = new DateOnly(2025, 5, 1),
                CheckOut = new DateOnly(2025, 5, 8), // 7 nachten
                Persons = 3
            };

            var rate = new Rate
            {
                PerPersonPerNight = 12m
            };

            var nights = 7;
            var gross = nights * request.Persons * rate.PerPersonPerNight; // 7 * 3 * 12 = 252

            var strategy = new CampsiteFreeNightDiscountStrategy();

            // ACT
            var discount = strategy.CalculateDiscount(request, rate, gross);

            // ASSERT
            // Onder interpretatie van de opdracht: altijd 1 nacht gratis, niet elke 4 nachten.
            // 1 nacht * 3 personen * €12 = €36
            Assert.Equal(36m, discount);
        }
    }
}