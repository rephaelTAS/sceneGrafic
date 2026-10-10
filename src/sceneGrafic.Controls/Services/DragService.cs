// sceneGrafic.Controls v0.1
// Arraste absoluto: guarda o offset entre o ponteiro e o canto do elemento
// no BeginDrag e aplica esse offset em UpdateDrag.
// Escreve em SceneElement.Layout e reflete no DesignElementControl.

using SceneGrafic.Controls.Design;
using SceneGrafic.Core.Model;

namespace SceneGrafic.Controls.Services;

public sealed class DragService : IDragService
{
    private readonly DesignSurface _surface;

    private SceneElement? _element;
    private DesignElementControl? _view;

    private double _offsetX;
    private double _offsetY;

    public bool IsDragging => _element is not null;

    public DragService(DesignSurface surface)
    {
        _surface = surface ?? throw new ArgumentNullException(nameof(surface));
    }

    public void BeginDrag(SceneElement element, double pointerX, double pointerY)
    {
        ArgumentNullException.ThrowIfNull(element);

        _view = _surface.GetView(element);
        if (_view is null) return;

        _element = element;
        _offsetX = pointerX - element.Layout.X;
        _offsetY = pointerY - element.Layout.Y;
    }

    public void UpdateDrag(double pointerX, double pointerY)
    {
        if (_element is null || _view is null) return;

        _element.Layout.X = pointerX - _offsetX;
        _element.Layout.Y = pointerY - _offsetY;

        _view.SetPosition(_element.Layout.X, _element.Layout.Y);
    }

    public void EndDrag()
    {
        _element = null;
        _view = null;
    }
}