// sceneGrafic.Core v0.1
// Representação abstrata de um componente visual no documento.
// Não conhece Avalonia — apenas descreve tipo, hierarquia, layout e propriedades.

namespace SceneGrafic.Core.Model;

public sealed class SceneElement
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Type { get; set; }

    public string? Name { get; set; }

    public SceneElement? Parent { get; internal set; }

    public IList<SceneElement> Children { get; } = new List<SceneElement>();

    public SceneLayout Layout { get; } = new();

    public IDictionary<string, object?> Properties { get; }
        = new Dictionary<string, object?>();

    public bool IsContainer => Children.Count > 0;
    
    // Inserir em: src/sceneGrafic.Core/Model/SceneElement.cs

    public void AddChild(SceneElement child)
    {
        ArgumentNullException.ThrowIfNull(child);

        if (ReferenceEquals(child, this))
            throw new InvalidOperationException("Elemento não pode ser filho de si mesmo.");

        child.Parent?.Children.Remove(child);
        child.Parent = this;
        Children.Add(child);
    }
}