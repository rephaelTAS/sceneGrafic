// sceneGrafic.Controls v0.1
// Área de design: hospeda os DesignElementControl e delega interações
// (seleção, drag) aos serviços correspondentes. Não contém lógica de negócio.

using Avalonia;
using Avalonia.Controls;
using SceneGrafic.Core.Model;

namespace SceneGrafic.Controls.Design;

public sealed class DesignSurface : Canvas
{
    private readonly Dictionary<Guid, DesignElementControl> _views = new();

    public SceneDocument? Document { get; private set; }

    public DesignSurface()
    {
        ClipToBounds = true;
    }

    public void Load(SceneDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);

        Document = document;
        Width  = document.Width;
        Height = document.Height;

        Children.Clear();
        _views.Clear();

        if (document.Root is null) return;

        foreach (var element in document.EnumerateAll())
            Add(element);
    }

    public DesignElementControl Add(SceneElement element)
    {
        ArgumentNullException.ThrowIfNull(element);

        var view = new DesignElementControl(element);
        _views[element.Id] = view;
        Children.Add(view);

        return view;
    }

    public void Remove(SceneElement element)
    {
        ArgumentNullException.ThrowIfNull(element);

        if (!_views.Remove(element.Id, out var view)) return;
        Children.Remove(view);
    }

    public DesignElementControl? GetView(SceneElement element)
        => _views.GetValueOrDefault(element.Id);

    public IReadOnlyCollection<DesignElementControl> Views
        => _views.Values;
}