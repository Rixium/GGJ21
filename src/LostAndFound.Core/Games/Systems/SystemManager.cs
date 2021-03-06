﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Systems
{
    public class SystemManager
    {
        private readonly IReadOnlyCollection<ISystem> _systems;

        public SystemManager(IReadOnlyCollection<ISystem> systems)
        {
            _systems = systems;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var system in _systems) system.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var system in _systems) system.Draw(spriteBatch);
        }

        public void Start()
        {
            foreach (var system in _systems)
            {
                system.Start();
            }
        }

        public T GetSystem<T>() => (T) _systems.First(x => x.GetType() == typeof(T));
    }
}