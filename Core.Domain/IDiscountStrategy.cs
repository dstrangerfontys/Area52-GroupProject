namespace Area52.Core.Domain
{
    /// <summary>
    /// Strategie-interface voor kortingen (FR-02).
    /// Elke korting bepaalt zelf:
    /// - of hij van toepassing is (CanApply)
    /// - hoeveel korting er gegeven wordt (CalculateDiscount)
    /// </summary>
    public interface IDiscountStrategy
    {
        bool CanApply(QuoteRequest request, Rate rate);

        decimal CalculateDiscount(QuoteRequest request, Rate rate, decimal grossAmount);
    }
}