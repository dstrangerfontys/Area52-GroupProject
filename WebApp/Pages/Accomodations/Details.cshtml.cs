using Area52.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Area52.WebApp.Pages.Accommodations
{
    /// <summary>
    /// Detailpagina van één accommodatie.
    /// Toont info + prijsindicatie met een 'Boek nu' knop.
    /// </summary>
    public class DetailsModel : PageModel
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IQuoteService _quoteService;
        private readonly IReservationService _reservationService;

        public Accommodation? Accommodation { get; private set; }
        public QuoteResult? Quote { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly CheckIn { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly CheckOut { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Persons { get; set; }

        public int? ReservationId { get; private set; }
        public string? ReservationStatus { get; private set; }

        public DetailsModel(
            IAccommodationRepository accommodationRepository,
            IQuoteService quoteService,
            IReservationService reservationService)
        {
            _accommodationRepository = accommodationRepository;
            _quoteService = quoteService;
            _reservationService = reservationService;
        }

        public IActionResult OnGet()
        {
            Accommodation = _accommodationRepository.GetById(Id);
            if (Accommodation == null)
            {
                return NotFound();
            }

            var request = new QuoteRequest
            {
                Type = Accommodation.Type,
                CheckIn = CheckIn,
                CheckOut = CheckOut,
                Persons = Persons
            };

            Quote = _quoteService.CalculateQuote(request);

            return Page();
        }

        public IActionResult OnPostBook()
        {
            Accommodation = _accommodationRepository.GetById(Id);
            if (Accommodation == null)
            {
                return NotFound();
            }

            var request = new QuoteRequest
            {
                Type = Accommodation.Type,
                CheckIn = CheckIn,
                CheckOut = CheckOut,
                Persons = Persons
            };

            var reservation = _reservationService.CreateReservation(request);

            ReservationId = reservation.Id;
            ReservationStatus = reservation.Status.ToString();

            Quote = new QuoteResult
            {
                GrossAmount = reservation.GrossAmount,
                DiscountAmount = reservation.DiscountAmount,
                NetAmount = reservation.NetAmount
            };

            return Page();
        }
    }
}