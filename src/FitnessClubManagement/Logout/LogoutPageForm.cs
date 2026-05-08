using FitnessClubManagement.Controls;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class LogoutPageForm : ModuleFormBase
{
    public LogoutPageForm() : base(
        "Logout",
        "Iesirea din cont si revenirea la ecranul de autentificare.",
        [new("Actiune", "Logout"), new("Siguranta", "Controlata"), new("Flux", "Login")],
        ["Inchidere sesiune curenta", "Revenire la LoginForm", "Curatare context utilizator"])
    {
    }
}
