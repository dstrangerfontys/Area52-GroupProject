using Microsoft.AspNetCore.Mvc;
using Area52.Core.Domain;
using Area52.WebApp.Models;

namespace Area52.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly IQuoteService _quoteService;

    public HomeController(IQuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new BookingViewModel());
    }

    [HttpPost]
    public IActionResult Index(BookingViewModel model)
    {
        if (!ModelState.IsValid || model.CheckIn == null || model.CheckOut == null || !model.Type.HasValue)
        {
            return View(model);
        }

        try
        {
            var request = new QuoteRequest
            {
                Type = model.Type.Value,
                CheckIn = model.CheckIn.Value,
                CheckOut = model.CheckOut.Value,
                Persons = model.Type == AccommodationType.Campsite ? model.Guests : 0
            };

            var result = _quoteService.CalculateQuote(request);
            model.GrossAmount = result.GrossAmount;
            model.DiscountAmount = result.DiscountAmount;
            model.NetAmount = result.NetAmount;
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        return View(model);
    }
}