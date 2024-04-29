using System.Text;

namespace TextEditor.Scene;

internal class Tab
{
    internal StringBuilder Content { get; set; }

    internal Tab(string content)
    {
        Content = new StringBuilder(content);
    }
}