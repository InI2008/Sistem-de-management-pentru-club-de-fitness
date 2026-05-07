using System.Collections.Generic;
using FitnessClubManagement.Models;

namespace FitnessClubManagement.Data;

internal static class SampleRepository
{
    public static MembershipInfo GetMembership() =>
        new("Andrei Rusu", "Premium 12 luni", "14.12.2026", "Achitat");

    public static IReadOnlyList<ClassBooking> GetBookings() =>
    [
        new("Body Pump", "Luni 18:00", "Elena Munteanu"),
        new("Yoga Flow", "Miercuri 19:30", "Irina Luca"),
        new("HIIT Blast", "Vineri 17:00", "Victor Grosu")
    ];

    public static IReadOnlyList<SummaryMetric> GetUserMetrics() =>
    [
        new("Vizite luna aceasta", "14", "Prezenta constanta"),
        new("Clase finalizate", "9", "Rezervari confirmate"),
        new("Sedinte PT", "3", "Antrenamente individuale"),
        new("Punctaj wellness", "92%", "Obiective aproape atinse")
    ];

    public static IReadOnlyList<SummaryMetric> GetAdminMetrics() =>
    [
        new("Membri activi", "1,284", "Crestere de 12%"),
        new("Clase azi", "18", "6 complet ocupate"),
        new("Venit lunar", "EUR 24.5K", "Peste tinta lunara"),
        new("Plati restante", "13", "Necesita urmarire")
    ];

    public static IReadOnlyList<AdminAlert> GetAdminAlerts() =>
    [
        new("Abonamente expirate", "41", "Trimite reminder pentru reinnoire"),
        new("Mentenanta aparate", "5", "2 urgente in sala cardio"),
        new("Tichete suport", "8", "4 cereri de reprogramare"),
        new("Traineri disponibili", "9", "Program complet pentru saptamana")
    ];
}
