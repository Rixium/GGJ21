using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<IEntity> NextEntities
        {
            get
            {
                var newList = new List<IEntity>();
                newList.AddRange(Entities);
                newList.AddRange(_entitiesToAdd);
                return newList;
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in Entities)
            {
                entity.Update(gameTime);
            }

            CleanUp();

            Entities = Entities.OrderBy(x => x.Bottom).ToList();
        }

        private void CleanUp()
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, new Vector2(0, 0), Color.White);

            foreach (var entity in Entities)
            {
                entity.Draw(spriteBatch);
            }
        }

        public void RemoveEntity(IEntity entity) => _entitiesToRemove.Add(entity);

        public void AddEntity(IEntity entity)
        {
            entity.Zone = this;
            _entitiesToAdd.Add(entity);
            entity.Start();
        }

        public Collider GetColliderWithProperty(string propertyName) =>
            Colliders.FirstOrDefault(x => x.GetProperty(propertyName) != null);

        public void Start()
        {
            foreach (var entity in Entities)
            {
                entity.Start();
            }
        }
    }
}