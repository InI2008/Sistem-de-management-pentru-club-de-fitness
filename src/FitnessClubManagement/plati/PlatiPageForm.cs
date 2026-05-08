using FitnessClubManagement.Controls;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class PlatiPageForm : ModuleFormBase
{
    public PlatiPageForm() : base(
        "Plati",
        "Urmarirea platilor, facturilor si restantelor pentru serviciile clubului.",
        [new("Venit lunar", "EUR 24.5K"), new("Restante", "13"), new("Metode", "4")],
        ["Istoric plati", "Plati restante", "Confirmare incasare", "Facturi si chitante"])
    {
    }
}
