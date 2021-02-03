using Asepreadr;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class LightComponent : Component
    {
        public Texture2D Texture { get; set; }
        public Color LightColor { get; set; } = Color.White;
        public Vector2 Offset { get; set; }
        public int Size { get; set; } = 30;

        private readonly IContentChest _contentChest;

        public LightComponent(IContentChest contentChest)
        {
            _contentChest = contentChest;
        }

        public override void Start()
        {
            Texture = _contentChest.Get<Texture2D>("Utils/light");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}