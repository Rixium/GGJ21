using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Config
{
    public class WindowConfiguration : IWindowConfiguration
    {
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public Vector2 Center => new Vector2(WindowWidth, WindowHeight) / 2f;
    }
}