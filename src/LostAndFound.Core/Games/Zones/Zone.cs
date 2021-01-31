using System;
using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Zones
{
    public class Zone : IZone
    {
        private readonly IList<IEntity> _entitiesToRemove = new List<IEntity>();
        private readonly IList<IEntity> _entitiesToAdd = new List<IEntity>();

        public IList<IEntity> Entities { get; set; }
        public ZoneType ZoneType { get; set; }
        public Collider[] Colliders { get; set; }
        public Texture2D Image { get; set; }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in _entitiesToRemove)
            {
                Entities.Remove(entity);
            }

            foreach (var entity in _entitiesToAdd)
            {
                Entities.Add(entity);
            }

            _entitiesToRemove.Clear();
            _entitiesToAdd.Clear();

            foreach (var entity in Entities)
            {
                entity.Update(gameTime);
            }

            Entities = Entities.OrderBy(x => x.Bottom).ToList();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, new Vector2(0, 0), Color.White);

            foreach (var entity in Entities)
            {
                entity.Draw(spriteBatch);
            }
        }

        public Vector2 GetTeleportPoint(Direction direction)
        {
            foreach (var collider in Colliders)
            {
                var directionProperty = collider.GetProperty("Direction");
                if (directionProperty == null)
                {
                    continue;
                }

                Enum.TryParse<Direction>(directionProperty, out var entranceDirection);

                if (entranceDirection == direction)
                {
                    return collider.Bounds.ToVector2();
                }
            }

            return Vector2.Zero;
        }

        public void RemoveEntity(IEntity entity) => _entitiesToRemove.Add(entity);
        public void AddEntity(IEntity entity)
        {
            _entitiesToAdd.Add(entity);
            entity.Start();
        }

        public Collider GetColliderWithProperty(string propertyName)
        {
            foreach (var collider in Colliders)
            {
                var property = collider.GetProperty(propertyName);
                if (property != null)
                {
                    return collider;
                }
            }

            return null;
        }

        public void Start()
        {
            foreach (var entity in Entities)
            {
                entity.Start();
            }
        }
    }
}