namespace LostAndFound.Core.Content.ContentLoader
{
    public interface IContentLoader<T>
    {
        T GetContent(string path);
    }
}