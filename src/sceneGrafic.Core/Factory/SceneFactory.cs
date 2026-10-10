// sceneGrafic.Core v0.1
// Implementação padrão: cria SceneElement com layout vindo do definition.

using SceneGrafic.Core.Model;

namespace SceneGrafic.Core.Catalog;

public sealed class SceneFactory : ISceneFactory
{
    public SceneElement Create(ComponentDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        var element = new SceneElement
        {
            Type = definition.Name,
            Name = null
        };

        element.Layout.Width  = definition.DefaultWidth;
        element.Layout.Height = definition.DefaultHeight;

        return element;
    }
}