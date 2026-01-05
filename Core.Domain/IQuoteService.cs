namespace Area52.Core.Domain;

public interface IQuoteService
{
    QuoteResult CalculateQuote(QuoteRequest request);
}