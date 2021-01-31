using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class StaticDrawComponent : IComponent
    {
        public Sprite Image { get; set; }

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
            spriteBatch.Draw(Image.Texture, Entity.Position, Image.Source, Image.Color);
        }
    }
}