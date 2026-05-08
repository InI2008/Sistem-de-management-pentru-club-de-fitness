namespace FitnessClubManagement.Models;

public enum AppRole
{
    Admin,
    Member
}

public sealed record AppUser(string FullName, string Email, AppRole Role);

public sealed record SummaryMetric(string Title, string Value, string Subtitle);

public sealed record MembershipInfo(string MemberName, string PlanName, string ExpiryDate, string PaymentStatus);

public sealed record ClassBooking(string ClassName, string Schedule, string CoachName);

public sealed record AdminAlert(string Title, string Value, string Details);

public sealed record ModuleStat(string Title, string Value);
