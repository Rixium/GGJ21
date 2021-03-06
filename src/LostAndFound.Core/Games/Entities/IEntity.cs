﻿using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Zones;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Entities
{
    public interface IEntity
    {
        public Vector2 Position { get; set; }
        int Bottom { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        public void AddComponent(IComponent component);
        public T GetComponent<T>();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
        void Start();
        IZone Zone { get; set; }
        void Destroy();
        
        bool Destroyed { get; set; }
    }
}