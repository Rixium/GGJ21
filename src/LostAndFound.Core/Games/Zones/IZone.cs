using System.Collections.Generic;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Zones
{
    public interface IZone
    {
        IList<IEntity> Entities { get; set; }
        ZoneType ZoneType { get; set; }
        Collider[] Colliders { get; set; }
        Texture2D Image { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        Vector2 GetTeleportPoint(Direction direction);
        void RemoveEntity(IEntity entity);
        void AddEntity(IEntity entity);
        Collider GetColliderWithProperty(string propertyName);
    }
}