using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FitnessClubManagement.Controls;

public sealed class FpLogoControl : Control
{
    public FpLogoControl()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        MinimumSize = new Size(240, 180);
        BackColor = Color.Transparent;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var canvas = new Rectangle(10, 10, Width - 20, Height - 20);

        using var backgroundBrush = new LinearGradientBrush(
            canvas,
            Color.FromArgb(24, 114, 255),
            Color.FromArgb(8, 27, 146),
            90F);
        using var glowBrush = new SolidBrush(Color.FromArgb(70, 52, 148, 255));
        using var borderPen = new Pen(Color.FromArgb(70, 195, 225, 255), 2F);

        var glowRect = Rectangle.Inflate(canvas, 18, 18);
        e.Graphics.FillEllipse(glowBrush, glowRect);

        using var path = CreateRoundedRectangle(canvas, 42);
        e.Graphics.FillPath(backgroundBrush, path);
        e.Graphics.DrawPath(borderPen, path);

        DrawKettlebell(e.Graphics, new Rectangle(canvas.Left + 26, canvas.Top + 36, 120, 132));
        DrawTextLogo(e.Graphics, canvas);
        DrawAccentWave(e.Graphics, canvas);
    }

    private static void DrawKettlebell(Graphics graphics, Rectangle area)
    {
        using var handlePath = CreateRoundedRectangle(new Rectangle(area.Left + 12, area.Top, 86, 54), 28);
        using var innerHandlePath = CreateRoundedRectangle(new Rectangle(area.Left + 26, area.Top + 14, 58, 26), 14);
        using var bodyPath = CreateRoundedRectangle(new Rectangle(area.Left, area.Top + 44, 110, 82), 40);

        using var handleBrush = new LinearGradientBrush(area, Color.White, Color.FromArgb(0, 162, 255), 20F);
        using var innerHandleBrush = new SolidBrush(Color.FromArgb(11, 26, 119));
        using var bodyBrush = new LinearGradientBrush(area, Color.FromArgb(68, 202, 255), Color.FromArgb(12, 27, 162), 40F);
        using var outlinePen = new Pen(Color.FromArgb(242, 247, 255), 4F);

        graphics.FillPath(handleBrush, handlePath);
        graphics.FillPath(innerHandleBrush, innerHandlePath);
        graphics.FillPath(bodyBrush, bodyPath);
        graphics.DrawPath(outlinePen, handlePath);
        graphics.DrawPath(outlinePen, bodyPath);

        using var shineBrush = new SolidBrush(Color.FromArgb(180, 255, 255, 255));
        graphics.FillEllipse(shineBrush, new Rectangle(area.Left + 18, area.Top + 58, 20, 34));
        graphics.FillEllipse(shineBrush, new Rectangle(area.Left + 56, area.Top + 58, 26, 20));
    }

    private static void DrawTextLogo(Graphics graphics, Rectangle canvas)
    {
        using var textBrush = new SolidBrush(Color.White);
        using var shadowBrush = new SolidBrush(Color.FromArgb(70, 5, 10, 35));
        using var bigFont = new Font("Segoe UI", 54F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Pixel);

        var shadowPoint = new PointF(canvas.Left + 160, canvas.Top + 72);
        var textPoint = new PointF(canvas.Left + 154, canvas.Top + 66);

        graphics.DrawString("FP", bigFont, shadowBrush, shadowPoint);
        graphics.DrawString("FP", bigFont, textBrush, textPoint);
    }

    private static void DrawAccentWave(Graphics graphics, Rectangle canvas)
    {
        var orange = new[]
        {
            new Point(canvas.Left + 74, canvas.Top + 162),
            new Point(canvas.Left + 140, canvas.Top + 146),
            new Point(canvas.Left + 236, canvas.Top + 140),
            new Point(canvas.Left + 318, canvas.Top + 144)
        };

        var blue = new[]
        {
            new Point(canvas.Left + 80, canvas.Top + 176),
            new Point(canvas.Left + 148, canvas.Top + 154),
            new Point(canvas.Left + 250, canvas.Top + 150),
            new Point(canvas.Left + 324, canvas.Top + 154)
        };

        using var orangePen = new Pen(Color.FromArgb(255, 109, 24), 6F)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round
        };
        using var bluePen = new Pen(Color.FromArgb(50, 167, 255), 5F)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round
        };

        graphics.DrawCurve(orangePen, orange);
        graphics.DrawCurve(bluePen, blue);
    }

    private static GraphicsPath CreateRoundedRectangle(Rectangle rect, int radius)
    {
        var path = new GraphicsPath();
        var diameter = radius * 2;

        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        return path;
    }
}
