// sceneGrafic.Controls v0.1
// Camada visual de seleção: borda + handles de resize.
// Não conhece SceneElement — apenas recebe um DesignElementControl alvo.

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace SceneGrafic.Controls.Design;

public sealed class SelectionAdorner : Canvas
{
    private const double HandleSize = 8;
    private const double BorderThickness = 1;

    private readonly Rectangle _border;
    private readonly List<Rectangle> _handles = new();

    private DesignElementControl? _target;

    public SelectionAdorner()
    {
        IsHitTestVisible = false;
        IsVisible = false;

        _border = new Rectangle
        {
            Stroke = Brushes.DodgerBlue,
            StrokeThickness = BorderThickness,
            Fill = Brushes.Transparent
        };

        Children.Add(_border);

        foreach (ResizeDirection dir in Enum.GetValues<ResizeDirection>())
            AddHandle(dir);
    }

    public void Attach(DesignElementControl target)
    {
        ArgumentNullException.ThrowIfNull(target);

        _target = target;
        IsVisible = true;
        Update();
    }

    public void Detach()
    {
        _target = null;
        IsVisible = false;
    }

    public void Update()
    {
        if (_target is null) return;

        var x = Canvas.GetLeft(_target);
        var y = Canvas.GetTop(_target);
        if (double.IsNaN(x)) x = 0;
        if (double.IsNaN(y)) y = 0;

        var w = _target.Width;
        var h = _target.Height;

        Canvas.SetLeft(_border, x);
        Canvas.SetTop(_border, y);
        _border.Width = w;
        _border.Height = h;

        PositionHandles(x, y, w, h);
    }

    private void AddHandle(ResizeDirection direction)
    {
        var handle = new Rectangle
        {
            Width = HandleSize,
            Height = HandleSize,
            Fill = Brushes.White,
            Stroke = Brushes.DodgerBlue,
            StrokeThickness = BorderThickness,
            Tag = direction
        };

        _handles.Add(handle);
        Children.Add(handle);
    }

    private void PositionHandles(double x, double y, double w, double h)
    {
        var half = HandleSize / 2;

        for (var i = 0; i < _handles.Count; i++)
        {
            var dir = (ResizeDirection)i;
            var (hx, hy) = dir switch
            {
                ResizeDirection.NorthWest => (x,           y),
                ResizeDirection.North     => (x + w / 2,   y),
                ResizeDirection.NorthEast => (x + w,       y),
                ResizeDirection.West      => (x,           y + h / 2),
                ResizeDirection.East      => (x + w,       y + h / 2),
                ResizeDirection.SouthWest => (x,           y + h),
                ResizeDirection.South     => (x + w / 2,   y + h),
                ResizeDirection.SouthEast => (x + w,       y + h),
                _ => (x, y)
            };

            Canvas.SetLeft(_handles[i], hx - half);
            Canvas.SetTop(_handles[i], hy - half);
        }
    }
}