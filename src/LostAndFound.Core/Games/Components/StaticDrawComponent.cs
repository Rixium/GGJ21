using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class StaticDrawComponent : IComponent
    {
        public Texture2D Image { get; set; }
        
        public IEntity Entity { get; set; }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Entity.Bottom = (int) (Entity.Position.Y + Image.Height);
            spriteBatch.Draw(Image, Entity.Position, Color.White);
        }
    }
}