using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Config
{
    public interface IWindowConfiguration
    {
        int WindowWidth { get; set; }
        int WindowHeight { get; set; }
        Vector2 Center { get; }
    }
}