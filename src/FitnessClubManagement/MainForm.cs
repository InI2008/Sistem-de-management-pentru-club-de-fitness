using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FitnessClubManagement.Controls;
using FitnessClubManagement.Data;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class MainForm : Form
{
    private static readonly string[] ProjectFolders =
    [
        LoginModule.DisplayName,
        RegisterModule.DisplayName,
        DashboardModule.DisplayName,
        ClientiModule.DisplayName,
        AbonementeModule.DisplayName,
        PrezenteModule.DisplayName,
        PlatiModule.DisplayName,
        AtrenoriModule.DisplayName,
        DarkModeModule.DisplayName,
        LogoutModule.DisplayName
    ];

    private readonly AppUser _currentUser;
    private bool _darkModeEnabled = true;

    public bool RequestLogout { get; private set; }

    public MainForm(AppUser currentUser)
    {
        _currentUser = currentUser;
        Text = "FP Fitness Club Management";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1320, 820);
        BackColor = Color.FromArgb(8, 16, 30);
        Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        BuildLayout();
    }

    private void BuildLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            BackColor = Color.FromArgb(8, 16, 30),
            Padding = new Padding(14)
        };

        root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

        root.Controls.Add(BuildSidebar(), 0, 0);
        root.Controls.Add(BuildContentArea(), 1, 0);

        Controls.Add(root);
    }

    private Control BuildSidebar()
    {
        var sidebar = CreateCard(Color.FromArgb(11, 23, 43));
        sidebar.Padding = new Padding(20);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 4
        };

        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));

        var logoHost = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.Transparent
        };
        logoHost.Controls.Add(new FpLogoControl { Dock = DockStyle.Fill });

        var accountPanel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(15, 32, 59),
            Padding = new Padding(16)
        };
        accountPanel.Controls.Add(new Label
        {
            Text = $"{_currentUser.FullName}\n{_currentUser.Email}\nRol: {GetRoleText(_currentUser.Role)}",
            Dock = DockStyle.Fill,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold)
        });

        var menuList = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            BackColor = Color.Transparent
        };

        foreach (var folder in ProjectFolders)
        {
            var visible = _currentUser.Role == AppRole.Admin || IsVisibleForMember(folder);
            if (!visible)
            {
                continue;
            }

            menuList.Controls.Add(CreateMenuButton(folder));
        }

        var logoutPanel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(15, 32, 59),
            Padding = new Padding(12)
        };
        logoutPanel.Controls.Add(new Label
        {
            Text = "Modern Fitness Management System",
            ForeColor = Color.FromArgb(111, 243, 197),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold)
        });

        layout.Controls.Add(logoHost, 0, 0);
        layout.Controls.Add(accountPanel, 0, 1);
        layout.Controls.Add(menuList, 0, 2);
        layout.Controls.Add(logoutPanel, 0, 3);

        sidebar.Controls.Add(layout);
        return sidebar;
    }

    private Control BuildContentArea()
    {
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 4,
            BackColor = Color.Transparent,
            Padding = new Padding(12, 0, 0, 0)
        };

        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        layout.Controls.Add(BuildHero(), 0, 0);
        layout.Controls.Add(BuildInfoStrip(), 0, 1);
        layout.Controls.Add(BuildMetricsSection(), 0, 2);
        layout.Controls.Add(BuildLowerSection(), 0, 3);

        return layout;
    }

    private Control BuildHero()
    {
        var hero = CreateCard(Color.FromArgb(10, 26, 50));
        hero.Padding = new Padding(24);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));

        var copy = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };

        copy.Controls.Add(new Label
        {
            Text = "MODERN FITNESS MANAGEMENT SYSTEM UI",
            ForeColor = Color.FromArgb(111, 243, 197),
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            AutoSize = true
        });
        copy.Controls.Add(new Label
        {
            Text = "Dashboard premium pentru club, inspirat din designul Figma.",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 24F, FontStyle.Bold),
            MaximumSize = new Size(720, 0),
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 0)
        });
        copy.Controls.Add(new Label
        {
            Text = "Layout-ul urmeaza directia dark, cu carduri mari, accent albastru si organizare vizuala de tip management dashboard.",
            ForeColor = Color.FromArgb(190, 206, 225),
            MaximumSize = new Size(700, 0),
            AutoSize = true,
            Margin = new Padding(0, 12, 0, 0)
        });

        var kpiPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1
        };
        kpiPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        kpiPanel.Controls.Add(CreateHighlightCard("Rata reinnoire", "87%", "Abonamente active si recurente"));

        layout.Controls.Add(copy, 0, 0);
        layout.Controls.Add(kpiPanel, 1, 0);
        hero.Controls.Add(layout);
        return hero;
    }

    private Control BuildInfoStrip()
    {
        var strip = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3
        };

        strip.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        strip.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        strip.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

        strip.Controls.Add(CreateInfoCard("Aplicatie", "C# Windows Forms pentru Visual Studio"));
        strip.Controls.Add(CreateInfoCard("Baza de date", "Schema SQL pregatita in database/schema.sql"));
        strip.Controls.Add(CreateInfoCard("Structura ceruta", string.Join(", ", ProjectFolders)));

        return strip;
    }

    private Control BuildMetricsSection()
    {
        var grid = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4
        };

        for (var index = 0; index < 4; index++)
        {
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        }

        var metrics = _currentUser.Role == AppRole.Admin
            ? SampleRepository.GetAdminMetrics()
            : SampleRepository.GetUserMetrics();

        foreach (var metric in metrics)
        {
            grid.Controls.Add(CreateMetricCard(metric));
        }

        return grid;
    }

    private Control BuildLowerSection()
    {
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3
        };

        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));

        layout.Controls.Add(BuildFoldersCard(), 0, 0);
        layout.Controls.Add(BuildMembershipOrAdminCard(), 1, 0);
        layout.Controls.Add(_currentUser.Role == AppRole.Admin ? BuildAdminAlertsCard() : BuildBookingsCard(), 2, 0);

        return layout;
    }

    private Control BuildFoldersCard()
    {
        var card = CreateCard(Color.FromArgb(11, 23, 43));
        card.Padding = new Padding(18);

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            BackColor = Color.Transparent
        };

        panel.Controls.Add(CreateTitle("Foldere obligatorii"));

        foreach (var folder in ProjectFolders)
        {
            if (_currentUser.Role != AppRole.Admin && !IsVisibleForMember(folder))
            {
                continue;
            }

            panel.Controls.Add(CreateFolderChip(folder));
        }

        card.Controls.Add(panel);
        return card;
    }

    private Control BuildMembershipOrAdminCard()
    {
        return _currentUser.Role == AppRole.Admin
            ? BuildAdminOverviewCard()
            : BuildMembershipCard();
    }

    private Control BuildMembershipCard()
    {
        var membership = SampleRepository.GetMembership();
        var card = CreateCard(Color.FromArgb(11, 23, 43));
        card.Padding = new Padding(18);

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };

        panel.Controls.Add(CreateTitle("Profil si abonament"));
        panel.Controls.Add(CreateBody($"Membru: {_currentUser.FullName}"));
        panel.Controls.Add(CreateBody($"Plan activ: {membership.PlanName}"));
        panel.Controls.Add(CreateBody($"Expira la: {membership.ExpiryDate}"));
        panel.Controls.Add(CreateBody($"Status plata: {membership.PaymentStatus}"));
        panel.Controls.Add(CreateBody("Module recomandate: abonemente, prezente, plati, atrenori"));

        card.Controls.Add(panel);
        return card;
    }

    private Control BuildAdminOverviewCard()
    {
        var card = CreateCard(Color.FromArgb(11, 23, 43));
        card.Padding = new Padding(18);

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };

        panel.Controls.Add(CreateTitle("Panou administrator"));
        panel.Controls.Add(CreateBody("Clienti: gestionare completa membri si profiluri."));
        panel.Controls.Add(CreateBody("Abonemente: creare pachete, preturi si expirari."));
        panel.Controls.Add(CreateBody("Prezente: evidenta check-in si participare la clase."));
        panel.Controls.Add(CreateBody("Plati: urmarire facturi, incasari si restante."));
        panel.Controls.Add(CreateBody("Atrenori: program, disponibilitate si specializari."));

        card.Controls.Add(panel);
        return card;
    }

    private Control BuildBookingsCard()
    {
        var card = CreateCard(Color.FromArgb(11, 23, 43));
        card.Padding = new Padding(18);

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            BackColor = Color.Transparent
        };

        panel.Controls.Add(CreateTitle("Clase rezervate"));

        foreach (var booking in SampleRepository.GetBookings())
        {
            panel.Controls.Add(CreateBookingPanel(booking));
        }

        card.Controls.Add(panel);
        return card;
    }

    private Control BuildAdminAlertsCard()
    {
        var card = CreateCard(Color.FromArgb(11, 23, 43));
        card.Padding = new Padding(18);

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            BackColor = Color.Transparent
        };

        panel.Controls.Add(CreateTitle("Alerte operationale"));

        foreach (var alert in SampleRepository.GetAdminAlerts())
        {
            panel.Controls.Add(CreateAlertBlock(alert));
        }

        card.Controls.Add(panel);
        return card;
    }

    private Panel CreateCard(Color backColor)
    {
        return new Panel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(8),
            BackColor = backColor
        };
    }

    private Control CreateInfoCard(string title, string value)
    {
        var card = CreateCard(Color.FromArgb(11, 23, 43));
        card.Padding = new Padding(18);

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };
        panel.Controls.Add(new Label
        {
            Text = title.ToUpperInvariant(),
            ForeColor = Color.FromArgb(111, 243, 197),
            Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
            AutoSize = true
        });
        panel.Controls.Add(new Label
        {
            Text = value,
            ForeColor = Color.White,
            MaximumSize = new Size(360, 0),
            AutoSize = true,
            Margin = new Padding(0, 8, 0, 0)
        });

        card.Controls.Add(panel);
        return card;
    }

    private Control CreateHighlightCard(string title, string value, string subtitle)
    {
        var card = CreateCard(Color.FromArgb(18, 42, 74));
        card.Padding = new Padding(18);

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };

        panel.Controls.Add(new Label
        {
            Text = title,
            ForeColor = Color.FromArgb(190, 206, 225),
            AutoSize = true
        });
        panel.Controls.Add(new Label
        {
            Text = value,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 28F, FontStyle.Bold),
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 0)
        });
        panel.Controls.Add(new Label
        {
            Text = subtitle,
            ForeColor = Color.FromArgb(111, 243, 197),
            AutoSize = true,
            Margin = new Padding(0, 8, 0, 0)
        });

        card.Controls.Add(panel);
        return card;
    }

    private Control CreateMetricCard(SummaryMetric metric)
    {
        var card = CreateCard(Color.FromArgb(11, 23, 43));
        card.Padding = new Padding(18);

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };

        panel.Controls.Add(new Label
        {
            Text = metric.Title,
            ForeColor = Color.FromArgb(190, 206, 225),
            AutoSize = true
        });
        panel.Controls.Add(new Label
        {
            Text = metric.Value,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 24F, FontStyle.Bold),
            AutoSize = true,
            Margin = new Padding(0, 12, 0, 0)
        });
        panel.Controls.Add(new Label
        {
            Text = metric.Subtitle,
            ForeColor = Color.FromArgb(111, 243, 197),
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 0)
        });

        card.Controls.Add(panel);
        return card;
    }

    private Control CreateFolderChip(string text)
    {
        return new Label
        {
            Text = text,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(16, 37, 68),
            Padding = new Padding(12, 10, 12, 10),
            Margin = new Padding(0, 0, 0, 10),
            AutoSize = true,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold)
        };
    }

    private Control CreateBookingPanel(ClassBooking booking)
    {
        var panel = new Panel
        {
            Width = 320,
            Height = 116,
            Margin = new Padding(0, 10, 0, 0),
            BackColor = Color.FromArgb(16, 37, 68)
        };

        panel.Controls.Add(new Label
        {
            Text = booking.ClassName,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 13F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(16, 16)
        });
        panel.Controls.Add(new Label
        {
            Text = $"Program: {booking.Schedule}",
            ForeColor = Color.FromArgb(190, 206, 225),
            AutoSize = true,
            Location = new Point(16, 52)
        });
        panel.Controls.Add(new Label
        {
            Text = $"Antrenor: {booking.CoachName}",
            ForeColor = Color.FromArgb(111, 243, 197),
            AutoSize = true,
            Location = new Point(16, 80)
        });

        return panel;
    }

    private Control CreateAlertBlock(AdminAlert alert)
    {
        var panel = new Panel
        {
            Width = 320,
            Height = 124,
            Margin = new Padding(0, 10, 0, 0),
            BackColor = Color.FromArgb(16, 37, 68)
        };

        panel.Controls.Add(new Label
        {
            Text = alert.Title,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(16, 14)
        });
        panel.Controls.Add(new Label
        {
            Text = alert.Value,
            ForeColor = Color.FromArgb(111, 243, 197),
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(16, 42)
        });
        panel.Controls.Add(new Label
        {
            Text = alert.Details,
            ForeColor = Color.FromArgb(190, 206, 225),
            MaximumSize = new Size(280, 0),
            AutoSize = true,
            Location = new Point(16, 80)
        });

        return panel;
    }

    private Control CreateMenuButton(string text)
    {
        var button = new Button
        {
            Text = text,
            Width = 216,
            Height = 44,
            Margin = new Padding(0, 0, 0, 10),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(16, 37, 68),
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold)
        };
        button.FlatAppearance.BorderSize = 0;
        button.Click += (_, _) => HandleMenuAction(text);
        return button;
    }

    private void HandleMenuAction(string moduleName)
    {
        switch (moduleName)
        {
            case "login":
                using (var form = new LoginPageForm())
                {
                    form.ShowDialog(this);
                }
                break;
            case "register":
                using (var form = new RegisterPageForm())
                {
                    form.ShowDialog(this);
                }
                break;
            case "dashboards":
                using (var form = new DashboardsPageForm())
                {
                    form.ShowDialog(this);
                }
                break;
            case "clienti":
                using (var form = new ClientiPageForm())
                {
                    form.ShowDialog(this);
                }
                break;
            case "abonemente":
                using (var form = new AbonementePageForm())
                {
                    form.ShowDialog(this);
                }
                break;
            case "prezente":
                using (var form = new PrezentePageForm())
                {
                    form.ShowDialog(this);
                }
                break;
            case "plati":
                using (var form = new PlatiPageForm())
                {
                    form.ShowDialog(this);
                }
                break;
            case "atrenori":
                using (var form = new AtrenoriPageForm())
                {
                    form.ShowDialog(this);
                }
                break;
            case "darl mode":
                ToggleDarkMode();
                using (var form = new DarkModePageForm())
                {
                    form.ShowDialog(this);
                }
                break;
            case "Logout":
                using (var form = new LogoutPageForm())
                {
                    form.ShowDialog(this);
                }
                RequestLogout = true;
                Close();
                break;
        }
    }

    private void ToggleDarkMode()
    {
        _darkModeEnabled = !_darkModeEnabled;
        BackColor = _darkModeEnabled ? Color.FromArgb(8, 16, 30) : Color.FromArgb(224, 232, 243);
    }

    private Label CreateTitle(string text)
    {
        return new Label
        {
            Text = text,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 15F, FontStyle.Bold),
            AutoSize = true
        };
    }

    private Label CreateBody(string text)
    {
        return new Label
        {
            Text = text,
            ForeColor = Color.FromArgb(190, 206, 225),
            MaximumSize = new Size(360, 0),
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 0)
        };
    }

    private static string GetRoleText(AppRole role)
    {
        return role == AppRole.Admin ? "Administrator" : "Membru";
    }

    private static bool IsVisibleForMember(string folder)
    {
        return folder is "login" or "register" or "dashboards" or "abonemente" or "atrenori" or "darl mode";
    }
}
