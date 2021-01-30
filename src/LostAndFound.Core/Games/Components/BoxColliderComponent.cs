using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class BoxColliderComponent : IComponent
    {
        public Rectangle Bounds => new Rectangle((int) (Entity.Position.X + Offset.X), (int)  (Entity.Position.Y + Offset.Y), Width, Height);
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Offset { get; set; }
        public IEntity Entity { get; set; }
        
        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}