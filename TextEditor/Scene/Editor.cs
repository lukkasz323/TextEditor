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
    }
}