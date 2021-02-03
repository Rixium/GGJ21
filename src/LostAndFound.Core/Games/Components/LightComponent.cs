using Asepreadr;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class LightComponent : IComponent
    {
        public IEntity Entity { get; set; }
        public Texture2D Texture { get; set; }
        public Color LightColor { get; set; } = Color.White;
        public Vector2 Offset { get; set; }
        public int Size { get; set; } = 30;

        private readonly IContentChest _contentChest;

        public LightComponent(IContentChest contentChest)
        {
            _contentChest = contentChest;
        }

        public void Start()
        {
            Texture = _contentChest.Get<Texture2D>("Utils/light");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}