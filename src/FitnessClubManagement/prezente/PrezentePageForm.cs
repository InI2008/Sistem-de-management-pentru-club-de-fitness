using FitnessClubManagement.Controls;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class PrezentePageForm : ModuleFormBase
{
    public PrezentePageForm() : base(
        "Prezente",
        "Evidenta intrarilor in sala, participarii la clase si monitorizarea frecventei.",
        [new("Check-in azi", "214"), new("Clase finalizate", "73"), new("Absente", "9")],
        ["Check-in client", "Prezenta pe clase", "Raport frecventa lunara", "Export prezente"])
    {
    }
}
