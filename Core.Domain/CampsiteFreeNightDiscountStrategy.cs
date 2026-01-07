using System;

namespace Area52.Core.Domain
{
    /// <summary>
    /// Korting voor kampeerplaatsen:
    /// - Bij een verblijf van 4 of meer nachten
    ///   krijgt de gast 1 nacht gratis (per plaats).
    ///
    /// Dit is de uitwerking van FR-02 voor kampeerplaatsen.
    /// </summary>
    public class CampsiteFreeNightDiscountStrategy : IDiscountStrategy
    {
        public string Name => "CAMPSITE_FREE_NIGHT";

        public bool CanApply(QuoteRequest request, Rate rate)
        {
            if (request.Type != AccommodationType.Campsite)
                return false;

            var nights = GetNights(request);
            return nights >= 4;
        }

        public decimal CalculateDiscount(QuoteRequest request, Rate rate, decimal grossAmount)
        {
            if (!CanApply(request, rate))
                return 0m;

            var nights = GetNights(request);

            if (nights <= 0)
                return 0m;

            var persons = Math.Max(1, request.Persons);
            var pricePerNight = rate.PerPersonPerNight;

            return persons * pricePerNight;
        }

        private static int GetNights(QuoteRequest request)
        {
            return request.CheckOut.DayNumber - request.CheckIn.DayNumber;
        }
    }
}