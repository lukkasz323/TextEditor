using System.Text;

namespace TextEditor.Scene;

internal class Tab
{
    internal StringBuilder Content { get; set; }
    internal string FileNameWithExtension { get; set; }

    internal Tab(string content, string fileNameWithExtension)
    {
        Content = new StringBuilder(content);
        FileNameWithExtension = fileNameWithExtension;
    }
}