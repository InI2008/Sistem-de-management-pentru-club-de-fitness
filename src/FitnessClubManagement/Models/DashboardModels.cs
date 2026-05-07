namespace FitnessClubManagement.Models;

public sealed record SummaryMetric(string Title, string Value, string Subtitle);

public sealed record MembershipInfo(string MemberName, string PlanName, string ExpiryDate, string PaymentStatus);

public sealed record ClassBooking(string ClassName, string Schedule, string CoachName);

public sealed record AdminAlert(string Title, string Value, string Details);
