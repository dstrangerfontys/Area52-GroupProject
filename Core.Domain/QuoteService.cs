namespace Area52.Core.Domain;

public class QuoteService : IQuoteService
{
    private readonly IRateRepository _rateRepository;
    private readonly IPricingStrategyFactory _strategyFactory;

    public QuoteService(IRateRepository rateRepository, IPricingStrategyFactory strategyFactory)
    {
        _rateRepository = rateRepository;
        _strategyFactory = strategyFactory;
    }

    public QuoteResult CalculateQuote(QuoteRequest request)
    {
        int nights = (request.CheckOut.ToDateTime(TimeOnly.MinValue)
                    - request.CheckIn.ToDateTime(TimeOnly.MinValue)).Days;

        if (nights <= 0)
            throw new ArgumentException("Check-out moet na check-in liggen.");

        var rate = _rateRepository.GetActiveRate(request.Type);
        var strategy = _strategyFactory.GetStrategy(request.Type);

        decimal gross = strategy.CalculateGross(request, rate);

        decimal discount = 0m;

        if ((request.Type == AccommodationType.Bungalow ||
             request.Type == AccommodationType.Chalet) &&
            nights >= 7)
        {
            discount += rate.WeekDiscount;
        }

        if (request.Type == AccommodationType.Campsite && nights >= 4)
        {
            discount += request.Persons * rate.PerPersonPerNight;
        }

        var net = Math.Max(0, gross - discount);

        return new QuoteResult
        {
            GrossAmount = decimal.Round(gross, 2),
            DiscountAmount = decimal.Round(discount, 2),
            NetAmount = decimal.Round(net, 2)
        };
    }
}