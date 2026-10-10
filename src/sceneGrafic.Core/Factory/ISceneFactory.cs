// sceneGrafic.Core v0.1
// Cria SceneElement a partir de um ComponentDefinition.

namespace SceneGrafic.Core.Catalog;
using SceneGrafic.Core.Model;

public interface ISceneFactory
{
    SceneElement Create(ComponentDefinition definition);
}