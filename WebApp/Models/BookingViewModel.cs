using System.ComponentModel.DataAnnotations;
using Area52.Core.Domain;

namespace Area52.WebApp.Models;

/// <summary>
/// ViewModel voor de startpagina. 
/// 
/// Dit model is specifiek voor de UI en vormt de binding tussen:
/// - het formulier in Index.cshtml (CheckIn, CheckOut, Guests, Type),
/// - en de berekende reserveringsinformatie (ReservationId, prijzen).
/// Door een aparte ViewModel te gebruiken in plaats van direct het
/// domeinmodel Reservation aan de view te koppelen, blijft de scheiding
/// tussen UI en domein duidelijk en kunnen we validatie en presentatie
/// onafhankelijk van de kernlogica aanpassen.
/// </summary>

public class BookingViewModel
{
    [Required]
    public DateOnly? CheckIn { get; set; }

    [Required]
    public DateOnly? CheckOut { get; set; }

    [Range(1, 20)]
    public int Guests { get; set; } = 2;

    [Required]
    public AccommodationType? Type { get; set; }

    public decimal? GrossAmount { get; set; }
    public decimal? DiscountAmount { get; set; }
    public decimal? NetAmount { get; set; }

    public int? ReservationId { get; set; }
    public string? ReservationStatus { get; set; }

    public List<AccommodationSearchResultViewModel> SearchResults { get; set; }
    = new();
}