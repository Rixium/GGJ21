using System.Collections.Generic;
using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Zones
{
    public class Zone : IZone
    {
        public IList<IEntity> Entities { get; set; }
        public ZoneType ZoneType { get; set; }
        public Collider[] Colliders { get; set; }
        public Texture2D Image { get; set; }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in Entities)
            {
                entity.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, new Vector2(0, 0), Color.White);

            foreach (var entity in Entities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}