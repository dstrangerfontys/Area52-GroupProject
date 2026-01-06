using Area52.Core.Domain;
using Area52.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Area52.WebApp.Pages;

/// <summary>
/// Razor PageModel voor de startpagina.
/// 
/// Verantwoordelijkheden:
/// - tonen van de beginpagina (OnGet),
/// - verwerken van het boekingsformulier (OnPost),
/// - aanroepen van IReservationService om een reservering aan te maken,
/// - vullen van het BookingViewModel met het resultaat.
/// 
/// Deze klasse bevat geen businesslogica of SQL; alle domeinregels lopen
/// via de services in Core.Domain. Dit ondersteunt de 3-lagenarchitectuur:
/// WebApp (UI) → Core.Domain (logica) → Infrastructure.DataAccess (data).
/// https://www.ibm.com/think/topics/three-tier-architecture
/// </summary>

public class IndexModel : PageModel
{
    private readonly IAvailabilityService _availabilityService;
    private readonly IQuoteService _quoteService;
    private readonly IReservationService _reservationService;

    [BindProperty]
    public BookingViewModel Booking { get; set; } = new();

    public IndexModel(
        IAvailabilityService availabilityService,
        IQuoteService quoteService,
        IReservationService reservationService)
    {
        _availabilityService = availabilityService;
        _quoteService = quoteService;
        _reservationService = reservationService;
    }

    public void OnGet()
    {
        // evt. default waardes zetten
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var request = new QuoteRequest
        {
            Type = Booking.Type!.Value,
            CheckIn = Booking.CheckIn!.Value,
            CheckOut = Booking.CheckOut!.Value,
            Persons = Booking.Guests
        };

        var accommodations = _availabilityService
            .SearchAvailable(request)
            .ToList();

        var quote = _quoteService.CalculateQuote(request);

        Booking.SearchResults = accommodations.Select(a => new AccommodationSearchResultViewModel
        {
            AccommodationId = a.Id,
            Name = a.Name,
            Type = a.Type,
            Capacity = a.Capacity,
            NetPrice = quote.NetAmount
        }).ToList();

        Booking.ReservationId = null;
        Booking.ReservationStatus = null;
        Booking.GrossAmount = null;
        Booking.DiscountAmount = null;
        Booking.NetAmount = null;

        return Page();
    }

    // In de volgende fase voeg ik hier:
    // public IActionResult OnPostReserve(int accommodationId) { ... }
    // toe om echt te boeken.
}