using System.Collections.Generic;
using System.Linq;

namespace Area52.Core.Domain
{

    /// <summary>
    /// Implementatie van IQuoteService.
    /// 
    /// Verantwoordelijkheden:
    /// - tarief voor het gekozen type opvragen via IRateRepository,
    /// - de juiste IPricingStrategy ophalen via IPricingStrategyFactory,
    /// - brutoprijs laten berekenen (FR-01),
    /// - kortingen toepassen (FR-02),
    /// - een Reservation-object vullen met prijsopbouw.
    /// 
    /// De service kent geen details van de database of UI; dit is pure domeinlogica.
    /// </summary>
    

    public class QuoteService : IQuoteService
    {
        private readonly IPricingStrategyFactory _pricingStrategyFactory;
        private readonly IRateRepository _rateRepository;
        private readonly IEnumerable<IDiscountStrategy> _discountStrategies;

        public QuoteService(
            IPricingStrategyFactory pricingStrategyFactory,
            IRateRepository rateRepository,
            IEnumerable<IDiscountStrategy> discountStrategies)
        {
            _pricingStrategyFactory = pricingStrategyFactory;
            _rateRepository = rateRepository;
            _discountStrategies = discountStrategies ?? Enumerable.Empty<IDiscountStrategy>();
        }

        public QuoteResult CalculateQuote(QuoteRequest request)
        {
            // 1. Tarief ophalen voor het gekozen type
            // LET OP: gebruik hier de naam van jouw interface-methode (zie stap 2)
            var rate = _rateRepository.GetActiveRate(request.Type);

            // 2. Juiste pricing-strategy kiezen
            var pricingStrategy = _pricingStrategyFactory.GetStrategy(request.Type);

            // 3. Bruto prijs (FR-01)
            var grossAmount = pricingStrategy.CalculateGross(request, rate);

            // 4. Kortingen (FR-02) – pipeline van IDiscountStrategy
            decimal totalDiscount = 0m;

            foreach (var strategy in _discountStrategies)
            {
                if (strategy.CanApply(request, rate))
                {
                    var discountAmount = strategy.CalculateDiscount(request, rate, grossAmount);

                    if (discountAmount > 0)
                    {
                        totalDiscount += discountAmount;
                    }
                }
            }

            // 5. Korting mag nooit meer zijn dan bruto
            if (totalDiscount > grossAmount)
            {
                totalDiscount = grossAmount;
            }

            var netAmount = grossAmount - totalDiscount;

            // 6. Resultaat teruggeven
            return new QuoteResult
            {
                GrossAmount = grossAmount,
                DiscountAmount = totalDiscount,
                NetAmount = netAmount
            };
        }
    }
}