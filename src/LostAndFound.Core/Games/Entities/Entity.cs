using System.Collections.Generic;
using LostAndFound.Core.Games.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Entities
{
    public class Entity : IEntity
    {
        private readonly IList<IComponent> _components = new List<IComponent>();
        public Vector2 Position { get; set; }
        public GameInstance GameInstance { get; set; }

        public Entity(Vector2 position)
        {
            Position = position;
        }

        public void AddComponent(IComponent component)
        {
            _components.Add(component);
            component.Entity = this;
        }

        public T GetComponent<T>()
        {
            foreach (var component in _components)
            {
                if (component.GetType() == typeof(T))
                {
                    return (T) component;
                }
            }

            return default;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
            {
                component.Draw(spriteBatch);
            }
        }
    }
}