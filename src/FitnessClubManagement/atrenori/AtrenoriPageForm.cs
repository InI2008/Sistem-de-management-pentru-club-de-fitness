using FitnessClubManagement.Controls;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class AtrenoriPageForm : ModuleFormBase
{
    public AtrenoriPageForm() : base(
        "Atrenori",
        "Administrarea echipei de antrenori, specializari si program de lucru.",
        [new("Atrenori", "9"), new("Disponibili", "6"), new("Rating mediu", "4.8")],
        ["Profil antrenor", "Disponibilitate", "Clase alocate", "Specializari si evaluari"])
    {
    }
}
