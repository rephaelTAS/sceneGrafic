// sceneGrafic.Controls v0.1
// Painel de componentes: lista os itens do catálogo, agrupados por categoria.

using Avalonia.Controls;
using SceneGrafic.Core.Catalog;

namespace SceneGrafic.Controls.Toolbox;

public sealed class Toolbox : UserControl
{
    private readonly StackPanel _root = new();

    public IComponentCatalog? Catalog { get; private set; }

    public Toolbox()
    {
        Content = _root;
    }

    public void Load(IComponentCatalog catalog)
    {
        ArgumentNullException.ThrowIfNull(catalog);

        Catalog = catalog;
        _root.Children.Clear();

        foreach (var def in catalog.GetAll())
            _root.Children.Add(new ToolboxItem(def));
    }
}