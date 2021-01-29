using System.IO;

namespace LostAndFound.Core.System
{
    public class FileSystem : IFileSystem
    {
        public string ReadAllText(string path) => File.ReadAllText(path);
    }
}