namespace Area52.Core.Domain;

/// <summary>
/// Repository-interface voor het opslaan en lezen van reserveringen.
/// 
/// Hiermee wordt de domeinlaag gescheiden van de concrete database.
/// Dit maakt het systeem eenvoudiger te testen en later aan te passen
/// naar een andere opslag (bijv. een andere database).
/// </summary>

public interface IReservationRepository
{
    Reservation Add(Reservation reservation);
    IEnumerable<Reservation> GetAll();
}