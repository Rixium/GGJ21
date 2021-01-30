using System.Collections.Generic;
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
    }
}