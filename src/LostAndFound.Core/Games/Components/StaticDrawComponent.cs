using Asepreadr.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class StaticDrawComponent : Component
    {
        public Sprite Image { get; set; }

        

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Entity.Bottom = (int) (Entity.Position.Y + Image.Height);
            Entity.Height = Image.Height;
            Entity.Width = Image.Width;
            spriteBatch.Draw(Image.Texture, Entity.Position, Image.Source, Image.Color);
        }
    }
}