using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TextEditor.Scene;

internal class Button
{
    internal string Text { get; }
    internal Vector2 TextSize { get; }
    internal Rectangle Body { get; set; }

    internal bool IsHovered =>
        Utils.IsColliding(new Point(Mouse.GetState().X, Mouse.GetState().Y), Body);

    internal Button(SpriteFont spriteFont, string text) 
    {
        Text = text;
        TextSize = spriteFont.MeasureString(text);
        Body = new Rectangle(0, 0, (int)TextSize.X, (int)TextSize.Y);
    }
}