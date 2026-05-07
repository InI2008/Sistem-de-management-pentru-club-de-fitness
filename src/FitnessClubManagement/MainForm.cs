using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FitnessClubManagement.Data;
using FitnessClubManagement.Models;

namespace FitnessClubManagement;

public sealed class MainForm : Form
{
    public MainForm()
    {
        Text = "FP Fitness Club Management";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1180, 760);
        BackColor = Color.FromArgb(14, 27, 48);
        Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        BuildLayout();
    }

    private void BuildLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            BackColor = Color.FromArgb(14, 27, 48),
            Padding = new Padding(18)
        };

        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 168F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        root.Controls.Add(BuildHeader(), 0, 0);
        root.Controls.Add(BuildDatabasePanel(), 0, 1);
        root.Controls.Add(BuildTabs(), 0, 2);

        Controls.Add(root);
    }

    private Control BuildHeader()
    {
        var panel = CreateCard();
        panel.Padding = new Padding(18);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

        var textPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };

        textPanel.Controls.Add(new Label
        {
            Text = "Sistem de management pentru club de fitness",
            ForeColor = Color.FromArgb(111, 243, 197),
            Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
            AutoSize = true
        });

        textPanel.Controls.Add(new Label
        {
            Text = "Aplicatie C# pentru Visual Studio cu zona User, zona Administrator si schema bazei de date.",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            MaximumSize = new Size(640, 0),
            AutoSize = true,
            Margin = new Padding(0, 8, 0, 0)
        });

        textPanel.Controls.Add(new Label
        {
            Text = "Logo-ul FP este integrat in proiect, iar structura SQL ramane pregatita pentru prezentare si dezvoltare ulterioara.",
            ForeColor = Color.FromArgb(198, 211, 229),
            MaximumSize = new Size(640, 0),
            AutoSize = true,
            Margin = new Padding(0, 12, 0, 0)
        });

        var brandPanel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.Transparent
        };

        var logo = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.Zoom,
            Dock = DockStyle.Fill,
            Image = LoadLogo()
        };

        brandPanel.Controls.Add(logo);
        layout.Controls.Add(textPanel, 0, 0);
        layout.Controls.Add(brandPanel, 1, 0);
        panel.Controls.Add(layout);

        return panel;
    }

    private Control BuildDatabasePanel()
    {
        var panel = CreateCard();
        panel.Padding = new Padding(20, 14, 20, 14);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3
        };

        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));

        layout.Controls.Add(CreateInfoBlock("Aplicatie", "Windows Forms, C#, Visual Studio"), 0, 0);
        layout.Controls.Add(CreateInfoBlock("Baza de date", "PostgreSQL/MySQL schema in database/schema.sql"), 1, 0);
        layout.Controls.Add(CreateInfoBlock("Module", "User, Administrator, Membri, Clase, Plati, Raportare"), 2, 0);

        panel.Controls.Add(layout);
        return panel;
    }

    private Control BuildTabs()
    {
        var tabs = new TabControl
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
            Appearance = TabAppearance.Normal
        };

        var userTab = new TabPage("Portal User")
        {
            BackColor = Color.FromArgb(9, 19, 33)
        };
        userTab.Controls.Add(BuildUserView());

        var adminTab = new TabPage("Panou Administrator")
        {
            BackColor = Color.FromArgb(9, 19, 33)
        };
        adminTab.Controls.Add(BuildAdminView());

        tabs.TabPages.Add(userTab);
        tabs.TabPages.Add(adminTab);
        return tabs;
    }

    private Control BuildUserView()
    {
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            Padding = new Padding(14)
        };

        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));

        var left = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2
        };
        left.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
        left.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        left.Controls.Add(BuildMembershipCard(), 0, 0);
        left.Controls.Add(BuildUserMetricsGrid(), 0, 1);

        var right = BuildBookingsCard();

        layout.Controls.Add(left, 0, 0);
        layout.Controls.Add(right, 1, 0);
        return layout;
    }

    private Control BuildAdminView()
    {
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(14)
        };

        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        layout.Controls.Add(BuildAdminMetricsGrid(), 0, 0);
        layout.Controls.Add(BuildAdminAlertsGrid(), 0, 1);
        return layout;
    }

    private Control BuildMembershipCard()
    {
        var membership = SampleRepository.GetMembership();
        var card = CreateCard();
        card.Padding = new Padding(18);

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };

        panel.Controls.Add(CreateTitle("Profil si abonament"));
        panel.Controls.Add(CreateBody($"Membru: {membership.MemberName}"));
        panel.Controls.Add(CreateBody($"Plan activ: {membership.PlanName}"));
        panel.Controls.Add(CreateBody($"Expira la: {membership.ExpiryDate}"));
        panel.Controls.Add(CreateBody($"Status plata: {membership.PaymentStatus}"));

        card.Controls.Add(panel);
        return card;
    }

    private Control BuildUserMetricsGrid()
    {
        var grid = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 2,
            Padding = new Padding(0, 14, 0, 0)
        };

        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

        var metrics = SampleRepository.GetUserMetrics();
        for (var index = 0; index < metrics.Count; index++)
        {
            grid.Controls.Add(CreateMetricCard(metrics[index]), index % 2, index / 2);
        }

        return grid;
    }

    private Control BuildBookingsCard()
    {
        var card = CreateCard();
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

    private Control BuildAdminMetricsGrid()
    {
        var grid = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            Padding = new Padding(0)
        };

        for (var index = 0; index < 4; index++)
        {
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        }

        foreach (var metric in SampleRepository.GetAdminMetrics())
        {
            grid.Controls.Add(CreateMetricCard(metric));
        }

        return grid;
    }

    private Control BuildAdminAlertsGrid()
    {
        var grid = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 2,
            Padding = new Padding(0, 14, 0, 0)
        };

        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

        var alerts = SampleRepository.GetAdminAlerts();
        for (var index = 0; index < alerts.Count; index++)
        {
            grid.Controls.Add(CreateAdminAlertCard(alerts[index]), index % 2, index / 2);
        }

        return grid;
    }

    private Panel CreateCard()
    {
        return new Panel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(8),
            BackColor = Color.FromArgb(20, 37, 64)
        };
    }

    private Control CreateMetricCard(SummaryMetric metric)
    {
        var card = CreateCard();
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
            ForeColor = Color.FromArgb(198, 211, 229),
            AutoSize = true
        });
        panel.Controls.Add(new Label
        {
            Text = metric.Value,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            AutoSize = true
        });
        panel.Controls.Add(new Label
        {
            Text = metric.Subtitle,
            ForeColor = Color.FromArgb(111, 243, 197),
            AutoSize = true
        });

        card.Controls.Add(panel);
        return card;
    }

    private Control CreateAdminAlertCard(AdminAlert alert)
    {
        var card = CreateCard();
        card.Padding = new Padding(18);

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };

        panel.Controls.Add(CreateTitle(alert.Title));
        panel.Controls.Add(new Label
        {
            Text = alert.Value,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 26F, FontStyle.Bold),
            AutoSize = true,
            Margin = new Padding(0, 8, 0, 0)
        });
        panel.Controls.Add(CreateBody(alert.Details));

        card.Controls.Add(panel);
        return card;
    }

    private Control CreateBookingPanel(ClassBooking booking)
    {
        var panel = new Panel
        {
            Width = 420,
            Height = 110,
            Margin = new Padding(0, 10, 0, 0),
            BackColor = Color.FromArgb(12, 27, 46)
        };

        var title = new Label
        {
            Text = booking.ClassName,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 13F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(16, 14)
        };

        var schedule = new Label
        {
            Text = $"Program: {booking.Schedule}",
            ForeColor = Color.FromArgb(198, 211, 229),
            AutoSize = true,
            Location = new Point(16, 48)
        };

        var coach = new Label
        {
            Text = $"Antrenor: {booking.CoachName}",
            ForeColor = Color.FromArgb(111, 243, 197),
            AutoSize = true,
            Location = new Point(16, 74)
        };

        panel.Controls.Add(title);
        panel.Controls.Add(schedule);
        panel.Controls.Add(coach);
        return panel;
    }

    private Control CreateInfoBlock(string title, string value)
    {
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
            ForeColor = Color.FromArgb(111, 243, 197),
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            AutoSize = true
        });
        panel.Controls.Add(new Label
        {
            Text = value,
            ForeColor = Color.White,
            MaximumSize = new Size(320, 0),
            AutoSize = true,
            Margin = new Padding(0, 6, 0, 0)
        });

        return panel;
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
            ForeColor = Color.FromArgb(198, 211, 229),
            MaximumSize = new Size(560, 0),
            AutoSize = true,
            Margin = new Padding(0, 8, 0, 0)
        };
    }

    private static Image? LoadLogo()
    {
        var logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "fp-logo.png");
        return File.Exists(logoPath) ? Image.FromFile(logoPath) : null;
    }
}
