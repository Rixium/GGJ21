namespace LostAndFound.Core.System
{
    public interface IApplicationFolder
    {
        void SetDirectoryName(string applicationName);
        string Create();

        string Save<T>(string path, T data, bool shouldOverwrite);
    }
}