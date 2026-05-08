using FitnessClubManagement.Controls;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class DashboardsPageForm : ModuleFormBase
{
    public DashboardsPageForm() : base(
        "Dashboards",
        "Dashboard central cu indicatori cheie, activitate recenta si monitorizare operationala.",
        [new("KPI", "4"), new("Roluri", "Admin/User"), new("Tema", "Dark")],
        ["Carduri KPI", "Alerte operationale", "Structura pe module", "Acces rapid la meniul lateral"])
    {
    }
}
