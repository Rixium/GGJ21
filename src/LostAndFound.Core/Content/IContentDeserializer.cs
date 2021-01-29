namespace LostAndFound.Core.Content
{
    public interface IContentDeserializer
    {
        T Get<T>(string path);
    }
}