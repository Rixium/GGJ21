using LostAndFound.Core.Games.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Entities
{
    public interface IEntity
    {
        public Vector2 Position { get; set; }
        GameInstance GameInstance { get; set; }
        public void AddComponent(IComponent component);
        public T GetComponent<T>();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
        void Start();
    }
}