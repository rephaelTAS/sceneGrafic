// sceneGrafic.Controls v0.1
// Contrato de arraste de elementos no DesignSurface.
// Só move — resize fica para a Fase 5.

using SceneGrafic.Core.Model;

namespace SceneGrafic.Controls.Services;

public interface IDragService
{
    bool IsDragging { get; }

    void BeginDrag(SceneElement element, double pointerX, double pointerY);

    void UpdateDrag(double pointerX, double pointerY);

    void EndDrag();
}