using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    public class ZoneManager
    {
        private readonly IZoneLoader _zoneLoader;

        private readonly IList<IZone> _zones = new List<IZone>();
        private const ZoneType StartingZone = ZoneType.Street;
        public IZone ActiveZone => _zones.First(x => x.ZoneType == _currentZone);
        private ZoneType _currentZone = StartingZone;

        public ZoneManager(IZoneLoader zoneLoader)
        {
            _zoneLoader = zoneLoader;
        }

        public void Load()
        {
            var zoneData = _zoneLoader.LoadZones();

            foreach (var zone in zoneData)
            {
                _zones.Add(new Zone
                {
                    ZoneType = zone.ZoneType,
                    Entities = new List<IEntity>(),
                    Colliders = zone.Colliders,
                    Image = zone.BackgroundImage
                });
            }
        }

        public IZone GetZone(ZoneType zoneType) => _zones.First(x => x.ZoneType == zoneType);

        public void SetActiveZone(ZoneType zoneType) => _currentZone = zoneType;

        public void MoveEntityToZone(IZone oldZone, IZone zoneToGoTo, IEntity entity)
        {
            oldZone.RemoveEntity(entity);
            zoneToGoTo.AddEntity(entity);
        }

        public void Update(GameTime gameTime) => ActiveZone.Update(gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            ActiveZone.Draw(spriteBatch);
        }

        public void Start()
        {
            foreach (var zone in _zones)
            {
                zone.Start();
            }
        }

        public void AddToActiveZone(Entity player) => ActiveZone.Entities.Add(player);
    }
}