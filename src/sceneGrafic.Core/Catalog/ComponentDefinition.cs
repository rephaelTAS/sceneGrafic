// sceneGrafic.Core v0.1
// Descreve um tipo de componente disponível na Toolbox.

using Avalonia.Controls;

namespace SceneGrafic.Core.Catalog;

public sealed class ComponentDefinition
{
    public required string Name { get; init; }

    public required Type ControlType { get; init; }

    public string? Category { get; init; }

    public string? Description { get; init; }

    public double DefaultWidth { get; init; } = 100;
    public double DefaultHeight { get; init; } = 30;

    public bool IsContainer =>
        typeof(Panel).IsAssignableFrom(ControlType);
}