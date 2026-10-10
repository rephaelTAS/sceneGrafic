// sceneGrafic.Controls v0.1
// Contrato de seleção. Seleção única no MVP; múltipla entra na Fase 5.

using SceneGrafic.Core.Model;

namespace SceneGrafic.Controls.Services;

public interface ISelectionService
{
    SceneElement? Selected { get; }

    event EventHandler<SceneElement?>? SelectionChanged;

    void Select(SceneElement? element);

    void Clear();
}