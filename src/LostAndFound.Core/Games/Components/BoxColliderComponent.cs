using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class BoxColliderComponent : Component
    {
        public Rectangle Bounds => new Rectangle((int) (Entity.Position.X + Offset.X), (int)  (Entity.Position.Y + Offset.Y), Width, Height);
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Offset { get; set; }
        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}