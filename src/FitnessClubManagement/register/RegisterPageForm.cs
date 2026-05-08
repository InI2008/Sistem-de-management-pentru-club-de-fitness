using FitnessClubManagement.Controls;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class RegisterPageForm : ModuleFormBase
{
    public RegisterPageForm() : base(
        "Register",
        "Zona de inregistrare pentru clienti noi ai clubului si generare de cont membru.",
        [new("Campuri", "6"), new("Flux", "Nou client"), new("Status", "Planificat")],
        ["Nume si prenume", "Email si parola", "Telefon si data nasterii", "Alegere abonament initial"])
    {
    }
}
