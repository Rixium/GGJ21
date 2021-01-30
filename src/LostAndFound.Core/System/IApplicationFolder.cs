namespace LostAndFound.Core.System
{
    public interface IApplicationFolder
    {
        string Root { get; set; }
        void SetDirectoryName(string directoryName);
        string Create();
        string Save<T>(string path, T data, bool shouldOverwrite);
    }
}