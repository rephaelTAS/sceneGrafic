// sceneGrafic.App v0.1
// Code-behind: monta catálogo, cria documento inicial, conecta serviços.

using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using SceneGrafic.App.ViewModels;
using SceneGrafic.Controls.Design;
using SceneGrafic.Controls.Services;
using SceneGrafic.Core.Catalog;
using SceneGrafic.Core.Model;

namespace SceneGrafic.App.Views;

public partial class DesignerWindow : Window
{
    private readonly DesignerViewModel _vm = new();

    private ISelectionService _selection = null!;
    private IDragService _drag = null!;

    public DesignerWindow()
    {
        InitializeComponent();

        DataContext = _vm;

        BuildCatalog();
        BuildInitialDocument();
        WireServices();
    }

    private void BuildCatalog()
    {
        var catalog = new ComponentCatalog();

        catalog.Register(new ComponentDefinition
        {
            Name = "Button",
            ControlType = typeof(Button),
            Category = "Controls",
            DefaultWidth = 120,
            DefaultHeight = 40
        });

        catalog.Register(new ComponentDefinition
        {
            Name = "TextBlock",
            ControlType = typeof(TextBlock),
            Category = "Controls",
            DefaultWidth = 100,
            DefaultHeight = 24
        });

        catalog.Register(new ComponentDefinition
        {
            Name = "TextBox",
            ControlType = typeof(TextBox),
            Category = "Controls",
            DefaultWidth = 160,
            DefaultHeight = 32
        });

        ToolboxPanel.Load(catalog);
    }

    private void BuildInitialDocument()
    {
        var factory = new SceneFactory();
        var root = new SceneElement { Type = "Canvas" };

        var buttonDef = new ComponentDefinition
        {
            Name = "Button",
            ControlType = typeof(Button),
            DefaultWidth = 120,
            DefaultHeight = 40
        };

        var button = factory.Create(buttonDef);
        button.Layout.X = 60;
        button.Layout.Y = 60;
        root.AddChild(button);

        _vm.Document.SetRoot(root);
        _vm.Document.IsModified = false;

        Surface.Load(_vm.Document);
    }

    private void WireServices()
    {
        _selection = new SelectionService();
        _drag = new DragService(Surface);

        _selection.SelectionChanged += (_, element) =>
        {
            _vm.Selected = element;

            foreach (var view in Surface.Views)
                view.IsSelected = ReferenceEquals(view.Element, element);
        };

        Surface.PointerPressed  += OnSurfacePointerPressed;
        Surface.PointerMoved    += OnSurfacePointerMoved;
        Surface.PointerReleased += OnSurfacePointerReleased;
    }

    private DesignElementControl? HitTest(Point p)
    {
        foreach (var view in Surface.Views)
        {
            var x = Canvas.GetLeft(view);
            var y = Canvas.GetTop(view);

            if (p.X >= x && p.X <= x + view.Width &&
                p.Y >= y && p.Y <= y + view.Height)
                return view;
        }

        return null;
    }

    private void OnSurfacePointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var p = e.GetPosition(Surface);
        var hit = HitTest(p);

        if (hit is null)
        {
            _selection.Clear();
            return;
        }

        _selection.Select(hit.Element);
        _drag.BeginDrag(hit.Element, p.X, p.Y);

        e.Pointer.Capture(Surface);
    }

    private void OnSurfacePointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_drag.IsDragging) return;

        var p = e.GetPosition(Surface);
        _drag.UpdateDrag(p.X, p.Y);
    }

    private void OnSurfacePointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _drag.EndDrag();
        e.Pointer.Capture(null);
    }
    
}