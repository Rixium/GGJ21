using Microsoft.Xna.Framework.Content;

namespace LostAndFound.Core.Content
{
    public interface IContentChest
    {
        ContentManager ContentManager { get; set; }
        T Get<T>(string assetName);
    }
}