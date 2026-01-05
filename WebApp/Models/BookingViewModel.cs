using System.ComponentModel.DataAnnotations;
using Area52.Core.Domain;

namespace Area52.WebApp.Models;

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
}