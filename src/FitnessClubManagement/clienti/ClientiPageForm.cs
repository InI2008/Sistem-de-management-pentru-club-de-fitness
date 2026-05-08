using FitnessClubManagement.Controls;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class ClientiPageForm : ModuleFormBase
{
    public ClientiPageForm() : base(
        "Clienti",
        "Administrarea membrilor clubului, profiluri, contact si istoric de activitate.",
        [new("Clienti activi", "1,284"), new("Noi azi", "12"), new("Retentie", "87%")],
        ["Lista clienti", "Profil client", "Istoric abonamente", "Istoric prezente", "Date de contact si status"])
    {
    }
}
