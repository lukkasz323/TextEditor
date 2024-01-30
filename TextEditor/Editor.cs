using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace TextEditor;

internal class Editor
{
    internal void Run() 
    {
        WindowOptions windowOptions = WindowOptions.Default;
        windowOptions.Title = "Text Editor";
        windowOptions.Position = new Vector2D<int>(200);

        IWindow window = Window.Create(windowOptions);

        window.Run();
    }
}