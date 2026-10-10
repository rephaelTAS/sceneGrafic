// sceneGrafic.Core v0.1
// Documento raiz do sceneGrafic. Agrega metadados + árvore de elementos.

namespace SceneGrafic.Core.Model;

public sealed class SceneDocument
{
    public string Name { get; set; } = "Untitled";

    public double Width { get; set; } = 800;
    public double Height { get; set; } = 600;

    public SceneElement? Root { get; internal set; }

    public bool IsModified { get; set; }

    public IEnumerable<SceneElement> EnumerateAll()
    {
        if (Root is null) yield break;

        var stack = new Stack<SceneElement>();
        stack.Push(Root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            yield return current;

            for (var i = current.Children.Count - 1; i >= 0; i--)
                stack.Push(current.Children[i]);
        }
    }
    
    public void SetRoot(SceneElement root)
    {
        ArgumentNullException.ThrowIfNull(root);

        root.Parent = null;
        Root = root;
    }
}