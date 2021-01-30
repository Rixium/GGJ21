using LostAndFound.Core.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class BoxColliderComponent : IComponent
    {
        private readonly IContentChest _contentChest;
        public Rectangle Bounds => new Rectangle((int) (Entity.Position.X + Offset.X), (int)  (Entity.Position.Y + Offset.Y), Width, Height);
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Offset { get; set; }
        public IEntity Entity { get; set; }

        private Texture2D _pixel;

        public BoxColliderComponent(IContentChest contentChest)
        {
            _contentChest = contentChest;
            _pixel = contentChest.Get<Texture2D>("Utils/pixel");
        }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_pixel, Bounds, Color.Green);
        }
    }
}