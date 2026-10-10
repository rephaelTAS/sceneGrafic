// sceneGrafic.Controls v0.1
// Implementação em memória. Notifica mudanças para ViewModels/Views.

using SceneGrafic.Core.Model;

namespace SceneGrafic.Controls.Services;

public sealed class SelectionService : ISelectionService
{
    private SceneElement? _selected;

    public SceneElement? Selected => _selected;

    public event EventHandler<SceneElement?>? SelectionChanged;

    public void Select(SceneElement? element)
    {
        if (ReferenceEquals(_selected, element)) return;

        _selected = element;
        SelectionChanged?.Invoke(this, element);
    }

    public void Clear() => Select(null);
}