using System;
using System.Drawing;
using System.Windows.Forms;
using FitnessClubManagement.Controls;
using FitnessClubManagement.Data;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class LoginForm : Form
{
    private readonly TextBox _emailTextBox;
    private readonly TextBox _passwordTextBox;
    private readonly Label _messageLabel;

    public AppUser? SelectedUser { get; private set; }

    public LoginForm()
    {
        Text = "Autentificare";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(560, 700);
        BackColor = Color.FromArgb(8, 16, 30);
        Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        var shell = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(18),
            BackColor = Color.FromArgb(8, 16, 30)
        };

        var card = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(11, 23, 43),
            Padding = new Padding(26)
        };

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 7,
            ColumnCount = 1,
            BackColor = Color.FromArgb(11, 23, 43)
        };

        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        var logo = new FpLogoControl
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(24, 6, 24, 18)
        };

        var heading = new Label
        {
            Text = "Conectare in sistemul clubului",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            AutoSize = true
        };

        var subheading = new Label
        {
            Text = "Acceseaza dashboard-ul modern pentru administrarea clubului de fitness.",
            ForeColor = Color.FromArgb(190, 206, 225),
            AutoSize = true,
            Margin = new Padding(0, 6, 0, 0)
        };

        _emailTextBox = CreateTextBox("admin@pulsefit.local");
        _passwordTextBox = CreateTextBox("admin123");
        _passwordTextBox.UseSystemPasswordChar = true;

        var loginButton = new Button
        {
            Text = "Conectare",
            Dock = DockStyle.Fill,
            Height = 50,
            BackColor = Color.FromArgb(24, 114, 255),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 11F, FontStyle.Bold)
        };
        loginButton.FlatAppearance.BorderSize = 0;
        loginButton.Click += (_, _) => TryLogin();

        _messageLabel = new Label
        {
            Text = "Cont demo admin: admin@pulsefit.local / admin123\nCont demo user: user@pulsefit.local / user123",
            ForeColor = Color.FromArgb(198, 211, 229),
            Dock = DockStyle.Fill,
            AutoSize = true
        };

        var quickPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            ColumnCount = 2,
            Height = 44
        };
        quickPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        quickPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

        quickPanel.Controls.Add(CreateQuickButton("Demo Admin", "admin@pulsefit.local", "admin123"), 0, 0);
        quickPanel.Controls.Add(CreateQuickButton("Demo User", "user@pulsefit.local", "user123"), 1, 0);

        root.Controls.Add(logo, 0, 0);
        root.Controls.Add(CreateHeadingPanel(heading, subheading), 0, 1);
        root.Controls.Add(CreateFieldPanel("Email", _emailTextBox), 0, 2);
        root.Controls.Add(CreateFieldPanel("Parola", _passwordTextBox), 0, 3);
        root.Controls.Add(loginButton, 0, 4);
        root.Controls.Add(_messageLabel, 0, 5);
        root.Controls.Add(quickPanel, 0, 6);

        card.Controls.Add(root);
        shell.Controls.Add(card);
        Controls.Add(shell);
        AcceptButton = loginButton;
    }

    private static Control CreateHeadingPanel(Control heading, Control subheading)
    {
        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };
        panel.Controls.Add(heading);
        panel.Controls.Add(subheading);
        return panel;
    }

    private static TextBox CreateTextBox(string text)
    {
        return new TextBox
        {
            Text = text,
            Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.FromArgb(16, 37, 68),
            ForeColor = Color.White,
            Margin = new Padding(0, 6, 0, 0)
        };
    }

    private static Control CreateFieldPanel(string label, Control input)
    {
        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2
        };
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));

        panel.Controls.Add(new Label
        {
            Text = label,
            ForeColor = Color.FromArgb(111, 243, 197),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            AutoSize = true,
            Dock = DockStyle.Fill
        }, 0, 0);
        panel.Controls.Add(input, 0, 1);

        return panel;
    }

    private Button CreateQuickButton(string text, string email, string password)
    {
        var button = new Button
        {
            Text = text,
            Dock = DockStyle.Top,
            Height = 36,
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(16, 37, 68),
            Margin = new Padding(6)
        };
        button.FlatAppearance.BorderColor = Color.FromArgb(40, 88, 132);
        button.Click += (_, _) =>
        {
            _emailTextBox.Text = email;
            _passwordTextBox.Text = password;
            _messageLabel.Text = "Datele demo au fost completate. Apasa Conectare.";
            _messageLabel.ForeColor = Color.FromArgb(111, 243, 197);
        };

        return button;
    }

    private void TryLogin()
    {
        if (SampleRepository.TryAuthenticate(_emailTextBox.Text.Trim(), _passwordTextBox.Text, out var user))
        {
            SelectedUser = user;
            DialogResult = DialogResult.OK;
            Close();
            return;
        }

        _messageLabel.Text = "Email sau parola incorecta. Foloseste unul dintre conturile demo.";
        _messageLabel.ForeColor = Color.FromArgb(255, 144, 144);
    }
}
