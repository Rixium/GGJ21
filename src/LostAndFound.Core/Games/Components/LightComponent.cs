using LostAndFound.Core.Content;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class LightComponent : IComponent
    {
        public IEntity Entity { get; set; }
        public Color LightColor { get; set; } = Color.Orange;
        public Vector2 Offset { get; set; }
        public int Size { get; set; } = 220;

        private readonly IContentChest _contentChest;
        private Texture2D _texture;

        public LightComponent(IContentChest contentChest)
        {
            _contentChest = contentChest;
        }

        public void Start()
        {
            _texture = _contentChest.Get<Texture2D>("Utils/light");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture,
                new Rectangle((int) (Entity.Position.X + Offset.X) - Size / 2, (int) (Entity.Position.Y + Offset.Y) - Size / 2, Size, Size),
                LightColor);
        }
    }
}