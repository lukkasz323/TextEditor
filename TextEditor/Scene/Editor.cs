using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace TextEditor.Scene;

internal class Editor
{
    private static readonly string s_cacheDirectoryName = "editor_cache";

    internal List<Tab> Tabs = new();
    internal int CurrentTabNumber { get; set; }
    internal List<Button> Dropdowns { get; }

    internal Tab CurrentTab => Tabs[CurrentTabNumber];
    
    internal Editor(SpriteFont spriteFont)
    {
        Dropdowns = new() 
        {
            new(spriteFont, "File"),
            new(spriteFont, "Edit"),
            new(spriteFont, "About"),
        };
        RelocateDropdowns();

        
        DirectoryInfo cacheDirectory = Directory.CreateDirectory(s_cacheDirectoryName);
        LoadTabsFromCache(cacheDirectory);

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

        void LoadTabsFromCache(DirectoryInfo cacheDirectory)
        {
            FileInfo[] cachedFiles = cacheDirectory.GetFiles();
            foreach (FileInfo cachedFile in cachedFiles)
            {
                string content = File.ReadAllText(cachedFile.FullName);
                Tabs.Add(new Tab(content, cachedFile.Name));
            }
        }
    }

    internal void SaveTabsToCache()
    {
        foreach (Tab tab in Tabs)
        {
            File.WriteAllText($"{s_cacheDirectoryName}\\{tab.FileNameWithExtension}", tab.Content.ToString());
        }
    }
}