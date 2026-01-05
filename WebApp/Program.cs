using Area52.Core.Domain;
using Area52.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddScoped<IRateRepository, InMemoryRateRepository>();
builder.Services.AddScoped<IPricingStrategyFactory, PricingStrategyFactory>();
builder.Services.AddScoped<IQuoteService, QuoteService>();

builder.Services.AddScoped<IReservationRepository, InMemoryReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();