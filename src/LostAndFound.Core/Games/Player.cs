using System.Collections.Generic;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    public interface IEntity
    {
        public Vector2 Position { get; set; }
        GameInstance GameInstance { get; set; }
        public void AddComponent(IComponent component);
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }

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

    public class Player
    {
        private readonly GameData _gameData;
        private PlayerData PlayerData => _gameData.PlayerData;

        public Texture2D Image;

        public Rectangle Bounds =>
            new Rectangle((int) PlayerData.Position.X, (int) PlayerData.Position.Y, Image.Width, Image.Height);

        public Player(Texture2D texture, GameData gameData)
        {
            Image = texture;
            _gameData = gameData;
        }

        public void Update(PlayerData playerData, GameTime gameTime)
        {
        }
    }
}