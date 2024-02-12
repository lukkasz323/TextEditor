using Microsoft.Xna.Framework;

namespace TextEditor.Core;

internal class Button
{
    internal string Text { get; }
    internal Vector2 TextSize { get; }
    internal Rectangle Rectangle { get; }

    internal Button(string text, Vector2 textSize) 
    {
        Text = text;
        TextSize = textSize;
    }
}