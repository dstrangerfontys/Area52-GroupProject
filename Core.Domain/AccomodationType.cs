namespace Area52.Core.Domain;

/// <summary>
/// Geeft de verschillende soorten accommodaties in Area 52 aan.
/// 
/// Deze enum wordt overal gebruikt waar het type verblijf relevant is:
/// - bij reserveringen (Reservation.Type),
/// - bij tarieven (Rate.Type),
/// - bij prijsstrategieën (PricingStrategy),
/// - in de UI (dropdown voor type selectie).
/// 
/// Naam: 'AccommodationType' omdat het een formele aanduiding is van het
/// type accommodatie in de domeinlogica, niet alleen in de database.
/// </summary>

public enum AccommodationType
{
    Bungalow,
    Chalet,
    Campsite
}