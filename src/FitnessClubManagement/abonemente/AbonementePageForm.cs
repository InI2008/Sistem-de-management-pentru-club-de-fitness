using FitnessClubManagement.Controls;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class AbonementePageForm : ModuleFormBase
{
    public AbonementePageForm() : base(
        "Abonemente",
        "Gestionarea pachetelor de abonament, preturilor si datelor de expirare.",
        [new("Planuri", "5"), new("Active", "1,102"), new("Expirate", "41")],
        ["Creare plan nou", "Actualizare pret", "Monitorizare expirari", "Asociere plan cu client"])
    {
    }
}
