using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;

namespace TextEditor.Scene;

internal class Editor
{
    internal List<Button> Dropdowns { get; }
    internal StringBuilder Content { get; set; } = new();
    
    internal Editor(SpriteFont spriteFont)
    {
        Dropdowns = new() 
        {
            new(spriteFont, "File"),
            new(spriteFont, "Edit"),
            new(spriteFont, "About"),
        };
        RelocateDropdowns();

        void RelocateDropdowns()
        {
            Point origin = new(8, 3);
            int textSpacing = 8;
            int widthSpacing = 8;
            int widthOffset = 0;
            foreach (Button button in Dropdowns)
            {
                button.Body = new Rectangle(widthOffset + origin.X, button.Body.Y + origin.Y, (int)button.TextSize.X + widthSpacing, (int)button.TextSize.Y);

                widthOffset += (int)button.TextSize.X + textSpacing + widthSpacing;
            }
        }
    }
}