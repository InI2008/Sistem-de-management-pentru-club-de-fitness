using FitnessClubManagement.Controls;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class DarkModePageForm : ModuleFormBase
{
    public DarkModePageForm() : base(
        "Darl Mode",
        "Tema vizuala dark a aplicatiei cu accente albastre si carduri premium, conform directiei din Figma.",
        [new("Tema", "Dark"), new("Accent", "Blue"), new("Contrast", "Ridicat")],
        ["Fundal inchis", "Carduri contrastante", "Accente cyan/albastre", "Citire buna pentru dashboard"])
    {
    }
}
