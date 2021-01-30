namespace LostAndFound.Core.System
{
    public interface IFileSystem
    {
        string ReadAllText(string path);
        void Delete(string directory);
        bool Exists(string path);
    }
}