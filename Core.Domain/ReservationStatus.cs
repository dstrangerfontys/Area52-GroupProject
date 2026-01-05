namespace Area52.Core.Domain;

/// <summary>
/// Mogelijke statussen van een reservering in het systeem.
/// 
/// Sluit aan op de functionele eisen:
/// - Planned    = Gepland
/// - Rejected   = Afgewezen
/// - Completed  = Afgerond (na uitchecken)
/// 
/// Door een enum te gebruiken in plaats van losse strings voorkomen we
/// typefouten en is de status overal op dezelfde manier gedefinieerd.
/// </summary>

public enum ReservationStatus
{
    Planned,
    Rejected,
    Completed
}