using Area52.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Area52.WebApp.Pages.Accommodations
{
    public class DetailsModel : PageModel
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IQuoteService _quoteService;
        private readonly IReservationService _reservationService;

        public DetailsModel(
            IAccommodationRepository accommodationRepository,
            IQuoteService quoteService,
            IReservationService reservationService)
        {
            _accommodationRepository = accommodationRepository;
            _quoteService = quoteService;
            _reservationService = reservationService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly CheckIn { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly CheckOut { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Persons { get; set; }

        public Accommodation? Accommodation { get; set; }
        public QuoteResult? Quote { get; set; }

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

            int? customerId = null;

            if (User.Identity?.IsAuthenticated == true)
            {
                var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idClaim != null && int.TryParse(idClaim.Value, out var parsedId))
                {
                    customerId = parsedId;
                }
            }

            var reservation = _reservationService.CreateReservation(request, Accommodation.Id, customerId);

            return RedirectToPage("/Index", new { reservedId = reservation.Id });
        }
    }
}