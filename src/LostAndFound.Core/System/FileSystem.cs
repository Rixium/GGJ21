using System.IO;

namespace LostAndFound.Core.System
{
    public class FileSystem : IFileSystem
    {
        public string ReadAllText(string path) => File.ReadAllText(path);

        public void Delete(string directory) => Directory.Delete(directory, true);

        public bool Exists(string path) => File.Exists(path);
    }
}