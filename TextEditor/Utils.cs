using Microsoft.Xna.Framework;

namespace TextEditor;

internal static class Utils
{
    static internal bool IsColliding(Point point, Rectangle rect) =>
           point.X > rect.X && point.X < rect.X + rect.Width
        && point.Y > rect.Y && point.Y < rect.Y + rect.Height;
}
