// sceneGrafic.Core v0.1
// Contrato do catálogo de componentes disponíveis.

namespace SceneGrafic.Core.Catalog;

public interface IComponentCatalog
{
    IReadOnlyList<ComponentDefinition> GetAll();

    IReadOnlyList<ComponentDefinition> GetByCategory(string category);

    ComponentDefinition? Find(string name);

    void Register(ComponentDefinition definition);
}