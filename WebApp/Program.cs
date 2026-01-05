using Area52.Core.Domain;
using Area52.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("Area52Database");

builder.Services.AddScoped<IRateRepository>(sp =>
    new MySqlRateRepository(connectionString!));

builder.Services.AddScoped<IReservationRepository>(sp =>
    new MySqlReservationRepository(connectionString!));

builder.Services.AddScoped<IPricingStrategyFactory, PricingStrategyFactory>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
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