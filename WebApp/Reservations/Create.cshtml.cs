using System.ComponentModel.DataAnnotations;
using Area52.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Area52.WebApp.Pages.Reservations
{
    public class CreateModel : PageModel
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IQuoteService _quoteService;
        private readonly IReservationService _reservationService;

        public CreateModel(
            IAccommodationRepository accommodationRepository,
            IQuoteService quoteService,
            IReservationService reservationService)
        {
            _accommodationRepository = accommodationRepository;
            _quoteService = quoteService;
            _reservationService = reservationService;
        }

        // Deze worden via de querystring gevuld (GET) en via hidden fields bij POST
        [BindProperty(SupportsGet = true)]
        public int AccommodationId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly CheckIn { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly CheckOut { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Persons { get; set; }

        public Accommodation? Accommodation { get; set; }
        public QuoteResult? Quote { get; set; }

        // Klantgegevens uit het formulier
        [BindProperty]
        public CustomerInputModel Customer { get; set; } = new();

        public class CustomerInputModel
        {
            [Required]
            public string Name { get; set; } = string.Empty;

            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;

            public string? Phone { get; set; }
            public string? Address { get; set; }
        }

        public void OnGet()
        {
            // 1. Accommodatie ophalen
            Accommodation = _accommodationRepository.GetById(AccommodationId);

            if (Accommodation == null)
            {
                // Simpel: terug naar home
                Response.Redirect("/");
                return;
            }

            // 2. Prijsindicatie berekenen
            var request = new QuoteRequest
            {
                Type = Accommodation.Type,
                CheckIn = CheckIn,
                CheckOut = CheckOut,
                Persons = Persons
            };

            Quote = _quoteService.CalculateQuote(request);

            // 3. (Later) hier kunnen we klantgegevens pre-fillen op basis van ingelogde gebruiker
        }

        public IActionResult OnPost()
        {
            // Accommodatie opnieuw ophalen (voor als we moeten hertekenen)
            Accommodation = _accommodationRepository.GetById(AccommodationId);

            if (!ModelState.IsValid || Accommodation == null)
            {
                // Prijs opnieuw berekenen als het formulier ongeldig is
                var requestInvalid = new QuoteRequest
                {
                    Type = Accommodation?.Type ?? AccommodationType.Bungalow,
                    CheckIn = CheckIn,
                    CheckOut = CheckOut,
                    Persons = Persons
                };

                Quote = _quoteService.CalculateQuote(requestInvalid);
                return Page();
            }

            var request = new QuoteRequest
            {
                Type = Accommodation!.Type,
                CheckIn = CheckIn,
                CheckOut = CheckOut,
                Persons = Persons
            };

            var reservation = _reservationService.CreateReservation(request, AccommodationId, null);

            TempData["ReservationId"] = reservation.Id;
            return RedirectToPage("/Reservations/Confirmation");
        }
    }
}