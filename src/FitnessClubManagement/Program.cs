using System;
using System.Windows.Forms;

namespace FitnessClubManagement;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        while (true)
        {
            using var loginForm = new LoginForm();
            if (loginForm.ShowDialog() != DialogResult.OK || loginForm.SelectedUser is null)
            {
                break;
            }

            var mainForm = new MainForm(loginForm.SelectedUser);
            Application.Run(mainForm);

            if (!mainForm.RequestLogout)
            {
                break;
            }
        }
    }
}
