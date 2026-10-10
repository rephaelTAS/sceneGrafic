// sceneGrafic.Controls v0.1
// Item individual da Toolbox: representa 1 ComponentDefinition.

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using SceneGrafic.Core.Catalog;

namespace SceneGrafic.Controls.Toolbox;

public sealed class ToolboxItem : ContentControl
{
    public ComponentDefinition Definition { get; }

    public ToolboxItem(ComponentDefinition definition)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));

        Content = definition.Name;
        Padding = new Thickness(8, 4);
        Cursor = new Cursor(StandardCursorType.Hand);

        PointerPressed += OnPointerPressed;
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        // Fase 4: iniciará o drag-and-drop via DragService.
        e.Handled = true;
    }
}