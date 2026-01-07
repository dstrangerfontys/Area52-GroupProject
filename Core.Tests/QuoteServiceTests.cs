using System;
using System.Collections.Generic;
using Area52.Core.Domain;
using Xunit;

namespace Area52.Core.Tests
{
    /// <summary>
    /// Unit tests voor QuoteService:
    /// - combineert prijsstrategie (FR-01) met kortingstrategie (FR-02).
    /// </summary>
    public class QuoteServiceTests
    {
        [Fact]
        public void CalculateQuote_Campsite4Nights_AppliesFreeNightDiscount()
        {
            // ARRANGE
            var request = new QuoteRequest
            {
                Type = AccommodationType.Campsite,
                CheckIn = new DateOnly(2025, 5, 1),
                CheckOut = new DateOnly(2025, 5, 5), // 4 nachten
                Persons = 2
            };

            // Tarief: €10 per persoon per nacht
            var rate = new Rate
            {
                Type = AccommodationType.Campsite,
                PerPersonPerNight = 10m
            };

            // Fake RateRepository dat altijd hetzelfde tarief teruggeeft
            var rateRepository = new FakeRateRepository(rate);

            var pricingFactory = new FakePricingStrategyFactory(
                new CampsitePricingStrategy()
            );

            // Kortingen: alleen gratis nacht korting
            var discounts = new List<IDiscountStrategy>
            {
                new CampsiteFreeNightDiscountStrategy()
            };

            var quoteService = new QuoteService(
                pricingFactory,
                rateRepository,
                discounts
            );

            // Verwachting:
            // Bruto = 4 nachten * 2 pers * €10 = €80
            // Korting = 1 nacht * 2 pers * €10 = €20
            // Netto = €80 - €20 = €60

            // ACT
            var result = quoteService.CalculateQuote(request);

            // ASSERT
            Assert.Equal(80m, result.GrossAmount);
            Assert.Equal(20m, result.DiscountAmount);
            Assert.Equal(60m, result.NetAmount);
        }

        #region Fake helpers voor test

        private class FakeRateRepository : IRateRepository
        {
            private readonly Rate _rate;

            public FakeRateRepository(Rate rate)
            {
                _rate = rate;
            }

            public Rate GetActiveRate(AccommodationType type)
            {
                return _rate;
            }
        }

        private class FakePricingStrategyFactory : IPricingStrategyFactory
        {
            private readonly PricingStrategy _strategy;

            public FakePricingStrategyFactory(PricingStrategy strategy)
            {
                _strategy = strategy;
            }

            public PricingStrategy GetStrategy(AccommodationType type)
            {
                return _strategy;
            }
        }

        #endregion
    }
}