using Area52.Core.Domain;
using Area52.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Area52.WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly IQuoteService _quoteService;

    public IndexModel(IQuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    [BindProperty]
    public BookingViewModel Booking { get; set; } = new();

    public void OnGet()
    {
        // Startpagina, nog geen data nodig
    }

    public void OnPost()
    {
        if (!ModelState.IsValid ||
            Booking.CheckIn == null ||
            Booking.CheckOut == null ||
            !Booking.Type.HasValue)
        {
            return;
        }

        try
        {
            var request = new QuoteRequest
            {
                Type = Booking.Type.Value,
                CheckIn = Booking.CheckIn.Value,
                CheckOut = Booking.CheckOut.Value,
                Persons = Booking.Type == AccommodationType.Campsite
                    ? Booking.Guests
                    : 0
            };

            var result = _quoteService.CalculateQuote(request);

            Booking.GrossAmount = result.GrossAmount;
            Booking.DiscountAmount = result.DiscountAmount;
            Booking.NetAmount = result.NetAmount;
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
    }
}