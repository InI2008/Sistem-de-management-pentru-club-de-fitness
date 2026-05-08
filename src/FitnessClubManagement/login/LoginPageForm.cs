using FitnessClubManagement.Controls;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class LoginPageForm : ModuleFormBase
{
    public LoginPageForm() : base(
        "Login",
        "Modulul de autentificare permite acces controlat pentru administrator si membru.",
        [new("Roluri", "2"), new("Conturi demo", "2"), new("Status", "Activ")],
        ["Autentificare cu email si parola", "Validare rol Admin/Member", "Mesaje de eroare pentru credentiale gresite", "Acces rapid cu conturi demo"])
    {
    }
}
