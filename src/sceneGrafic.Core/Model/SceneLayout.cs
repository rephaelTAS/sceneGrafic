// sceneGrafic.Core v0.1
// Posicionamento e dimensões de um SceneElement.
// Não carrega propriedades do controle (Content, FontSize, etc.);
// essas vão em SceneElement.Properties.

using SceneGrafic.Core.Primitives;

namespace SceneGrafic.Core.Model;

public sealed class SceneLayout
{
    public double X { get; set; }
    public double Y { get; set; }

    public double Width { get; set; } = 100;
    public double Height { get; set; } = 30;

    public double Rotation { get; set; }

    public double ScaleX { get; set; } = 1;
    public double ScaleY { get; set; } = 1;

    public ScenePoint Position
    {
        get => new(X, Y);
        set { X = value.X; Y = value.Y; }
    }

    public SceneSize Size
    {
        get => new(Width, Height);
        set { Width = value.Width; Height = value.Height; }
    }
}