using Microsoft.Xna.Framework.Content;

namespace LostAndFound.Core.Content
{
    public class ContentChest : IContentChest
    {
        public ContentManager ContentManager { get; set; }
        public T Get<T>(string assetName)
        {
            return ContentManager.Load<T>(assetName);
        }
    }
}