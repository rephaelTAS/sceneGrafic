// sceneGrafic.Core v0.1
// Catálogo em memória, indexado por nome.

namespace SceneGrafic.Core.Catalog;

public sealed class ComponentCatalog : IComponentCatalog
{
    private readonly Dictionary<string, ComponentDefinition> _byName
        = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyList<ComponentDefinition> GetAll()
        => _byName.Values.ToList();

    public IReadOnlyList<ComponentDefinition> GetByCategory(string category)
        => _byName.Values
            .Where(d => string.Equals(d.Category, category, StringComparison.OrdinalIgnoreCase))
            .ToList();

    public ComponentDefinition? Find(string name)
        => _byName.GetValueOrDefault(name);

    public void Register(ComponentDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        _byName[definition.Name] = definition;
    }
}