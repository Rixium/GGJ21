using System.IO;
using Microsoft.Xna.Framework.Content;

namespace LostAndFound.Core.Content
{
    public class ContentChest : IContentChest
    {
        public ContentManager ContentManager { get; set; }
        public T Get<T>(string assetName)
        {
            var fixedPath = assetName.Replace($"Assets{Path.DirectorySeparatorChar}", "");
            return ContentManager.Load<T>(fixedPath);
        }
    }
}