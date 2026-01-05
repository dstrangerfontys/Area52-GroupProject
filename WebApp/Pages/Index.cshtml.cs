using Area52.Core.Domain;
using Area52.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Area52.WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly IReservationService _reservationService;

    public IndexModel(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [BindProperty]
    public BookingViewModel Booking { get; set; } = new();

    public void OnGet()
    {
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

            // NIEUW: reservering aanmaken via domeinservice
            var reservation = _reservationService.CreateReservation(request);

            // Prijs + reserveringsinfo terugzetten in ViewModel
            Booking.GrossAmount = reservation.GrossAmount;
            Booking.DiscountAmount = reservation.DiscountAmount;
            Booking.NetAmount = reservation.NetAmount;
            Booking.ReservationId = reservation.Id;
            Booking.ReservationStatus = reservation.Status.ToString();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
    }
}