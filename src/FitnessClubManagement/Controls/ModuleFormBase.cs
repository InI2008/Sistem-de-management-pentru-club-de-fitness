using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FitnessClubManagement.Models;

namespace FitnessClubManagement.Controls;

public abstract class ModuleFormBase : Form
{
    protected ModuleFormBase(string title, string subtitle, IReadOnlyList<ModuleStat> stats, IReadOnlyList<string> items)
    {
        Text = title;
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(940, 620);
        MinimumSize = new Size(820, 540);
        BackColor = Color.FromArgb(8, 16, 30);
        Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 3,
            Padding = new Padding(18),
            BackColor = Color.FromArgb(8, 16, 30)
        };

        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        root.Controls.Add(BuildHero(title, subtitle), 0, 0);
        root.Controls.Add(BuildStats(stats), 0, 1);
        root.Controls.Add(BuildItems(items), 0, 2);

        Controls.Add(root);
    }

    private static Control BuildHero(string title, string subtitle)
    {
        var panel = CreateCard();
        panel.Padding = new Padding(22);

        var host = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };
        host.Controls.Add(new Label
        {
            Text = title.ToUpperInvariant(),
            ForeColor = Color.FromArgb(111, 243, 197),
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            AutoSize = true
        });
        host.Controls.Add(new Label
        {
            Text = title,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 0)
        });
        host.Controls.Add(new Label
        {
            Text = subtitle,
            ForeColor = Color.FromArgb(190, 206, 225),
            MaximumSize = new Size(760, 0),
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 0)
        });

        panel.Controls.Add(host);
        return panel;
    }

    private static Control BuildStats(IReadOnlyList<ModuleStat> stats)
    {
        var grid = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = stats.Count
        };

        for (var index = 0; index < stats.Count; index++)
        {
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / stats.Count));
            grid.Controls.Add(CreateStatCard(stats[index]), index, 0);
        }

        return grid;
    }

    private static Control CreateStatCard(ModuleStat stat)
    {
        var panel = CreateCard();
        panel.Padding = new Padding(18);

        var host = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent
        };
        host.Controls.Add(new Label
        {
            Text = stat.Title,
            ForeColor = Color.FromArgb(190, 206, 225),
            AutoSize = true
        });
        host.Controls.Add(new Label
        {
            Text = stat.Value,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 0)
        });

        panel.Controls.Add(host);
        return panel;
    }

    private static Control BuildItems(IReadOnlyList<string> items)
    {
        var panel = CreateCard();
        panel.Padding = new Padding(18);

        var list = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            BackColor = Color.Transparent
        };

        list.Controls.Add(new Label
        {
            Text = "Sectiuni si actiuni",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 15F, FontStyle.Bold),
            AutoSize = true
        });

        foreach (var item in items)
        {
            list.Controls.Add(new Label
            {
                Text = item,
                ForeColor = Color.FromArgb(190, 206, 225),
                BackColor = Color.FromArgb(16, 37, 68),
                Padding = new Padding(12, 10, 12, 10),
                Margin = new Padding(0, 10, 0, 0),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            });
        }

        panel.Controls.Add(list);
        return panel;
    }

    private static Panel CreateCard()
    {
        return new Panel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(8),
            BackColor = Color.FromArgb(11, 23, 43)
        };
    }
}
