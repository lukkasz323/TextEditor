using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Drawing;

namespace TextEditor;

internal class Editor
{
    private IWindow _window;
    private GL? _gl;
    private uint _vao;
    private uint _vbo;
    private uint _program;
    private uint _ebo;
    private int _ticks;
    private int _slowFPSCounter;

    internal Editor()
    {
        _window = Window.Create(WindowOptions.Default with {
            Title = "Text Editor",
            Position = new Vector2D<int>(200),
        });
        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;
    }

    internal void Run() 
    {
        _window.Run();
    }

    internal void OnLoad()
    {
        _gl = _window.CreateOpenGL();
        _gl.ClearColor(Color.Indigo);

        _vao = _gl.GenVertexArray();
        _gl.BindVertexArray(_vao);

        _vbo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

        ReadOnlySpan<float> vertices =
        [
            0.5f,  0.5f, 0f,
            0.5f, -0.5f, 0f,
           -0.5f, -0.5f, 0f,
        ];

        _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(float)), vertices, BufferUsageARB.StaticDraw);

        ReadOnlySpan<uint> indices =
        [
            0u, 1u, 3u,
            1u, 2u, 3u
        ];

        _ebo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);

        _program = _gl.CreateProgram();
    }

    internal void OnUpdate(double deltaTime) 
    {
        ++_ticks;

        int fps = (int)Math.Floor(deltaTime * 3600);
        if (_ticks % 6 == 0)
        {
            _slowFPSCounter = fps;
        }
    }

    internal void OnRender(double deltaTime) 
    {
        _gl?.Clear(ClearBufferMask.ColorBufferBit);

        _gl.BindVertexArray(_vao);
        _gl.DrawBuffer(DrawBufferMode.Back);

        Console.WriteLine($"{_slowFPSCounter}, {_ticks}, {_window.Time}");
    }
}