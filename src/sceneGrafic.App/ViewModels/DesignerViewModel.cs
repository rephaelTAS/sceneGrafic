// sceneGrafic.App v0.1
// Estado mínimo do designer: documento + seleção atual.

using System.ComponentModel;
using System.Runtime.CompilerServices;
using SceneGrafic.Core.Model;

namespace SceneGrafic.App.ViewModels;

public sealed class DesignerViewModel : INotifyPropertyChanged
{
    public SceneDocument Document { get; } = new();

    private SceneElement? _selected;
    public SceneElement? Selected
    {
        get => _selected;
        set
        {
            if (ReferenceEquals(_selected, value)) return;
            _selected = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}