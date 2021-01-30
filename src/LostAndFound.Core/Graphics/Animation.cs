using System.Collections.Generic;

namespace LostAndFound.Core.Graphics
{
    public class Animation
    {
        public IList<Sprite> Sprites { get; }
        public float FrameDuration { get; set; } = 0.1f;
        public int FrameCount => Sprites.Count;

        public Animation(IList<Sprite> sprites)
        {
            Sprites = sprites;
        }
    }
}