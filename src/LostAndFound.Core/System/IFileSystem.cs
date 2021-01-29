namespace LostAndFound.Core.System
{
    public interface IFileSystem
    {
        string ReadAllText(string path);
    }
}