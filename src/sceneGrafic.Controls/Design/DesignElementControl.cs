// sceneGrafic.Controls v0.1
// Representação visual de um SceneElement no DesignSurface.
// Ponte entre o modelo (SceneElement) e o controle real do Avalonia.

using Avalonia;
using Avalonia.Controls;
using SceneGrafic.Core.Model;

namespace SceneGrafic.Controls.Design;

public sealed class DesignElementControl : ContentControl
{
    public SceneElement Element { get; }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value) return;
            _isSelected = value;
            UpdateSelectionVisual();
        }
    }

    private bool _isSelected;

    public DesignElementControl(SceneElement element)
    {
        Element = element ?? throw new ArgumentNullException(nameof(element));

        Width  = element.Layout.Width;
        Height = element.Layout.Height;

        Canvas.SetLeft(this, element.Layout.X);
        Canvas.SetTop(this, element.Layout.Y);
    }
    
    // Inserir em: src/sceneGrafic.Controls/Design/DesignElementControl.cs
// Logo abaixo do construtor.

    public void SetPosition(double x, double y)
    {
        Element.Layout.X = x;
        Element.Layout.Y = y;

        Avalonia.Controls.Canvas.SetLeft(this, x);
        Avalonia.Controls.Canvas.SetTop(this, y);
    }

    private void UpdateSelectionVisual()
    {
        // Placeholder: o adorner cuidará do visual de seleção na Fase 3.3.
    }
}