using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TextEditor.Scene;

internal class Editor
{
    internal List<Button> Dropdowns { get; }
    
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