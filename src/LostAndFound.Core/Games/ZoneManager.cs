using System;
using System.Collections.Generic;
using System.Linq;
using Asepreadr.Aseprite;
using Asepreadr.Loaders;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    public class ZoneManager
    {
        public Action<ZoneType> ZoneChanged { get; set; }

        private readonly IZoneLoader _zoneLoader;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;

        private readonly IList<IZone> _zones = new List<IZone>();
        private const ZoneType StartingZone = ZoneType.Street;
        public IZone ActiveZone => _zones.First(x => x.ZoneType == _currentZone);
        private ZoneType _currentZone = StartingZone;

        public ZoneManager(IZoneLoader zoneLoader, IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _zoneLoader = zoneLoader;
            _spriteMapLoader = spriteMapLoader;
        }

        public void Load()
        {
            var entitySpriteMap = _spriteMapLoader.GetContent("Assets\\Images\\entities.json");
            var peopleSpriteMap = _spriteMapLoader.GetContent("Assets\\Images\\People.json");
            var zoneData = _zoneLoader.LoadZones();

            foreach (var zone in zoneData)
            {
                var collidersWithLight = zone.Colliders.Where(x => x.GetProperty("Light") != null);

                var newZone = new Zone
                {
                    ZoneType = zone.ZoneType,
                    Entities = new List<IEntity>(),
                    Colliders = zone.Colliders,
                    Image = zone.BackgroundImage
                };

                foreach (var collider in collidersWithLight)
                {
                    var lightEntity = new Entity(collider.Bounds.Center.ToVector2());
                    var lightComponent = Program.Resolve<LightComponent>();
                    lightComponent.Size = 100;
                    lightComponent.LightColor = Color.Yellow;

                    lightEntity.AddComponent(lightComponent);
                    newZone.Entities.Add(lightEntity);
                }

                var zoneEntities = zone.Colliders.Where(x => x.GetProperty("Entity") != null);

                foreach (var collider in zoneEntities)
                {
                    var entity = new Entity(collider.Bounds.ToVector2());
                    var staticDrawComponent = Program.Resolve<StaticDrawComponent>();
                    var entityName = collider.Properties["Entity"];
                    staticDrawComponent.Image = entitySpriteMap.CreateSpriteFromRegion(entityName);

                    entity.AddComponent(staticDrawComponent);
                    newZone.Entities.Add(entity);
                }

                var keeper = zone.Colliders.Where(x => x.Name.Equals("ShopKeeper"));

                foreach (var collider in keeper)
                {
                    var entity = new Entity(collider.Bounds.ToVector2());
                    
                    var animator = Program.Resolve<AnimatorComponent>();
                    animator.AddAnimation("Idle",
                        new Animation(new[]
                            {
                                peopleSpriteMap.CreateSpriteFromRegion("ShopKeeper"),
                                peopleSpriteMap.CreateSpriteFromRegion("ShopKeeper2")
                            }
                        )
                        {
                            FrameDuration = 0.8f
                        });

                    entity.AddComponent(animator);
                    entity.AddComponent(Program.Resolve<AnimationComponent>());
                    entity.AddComponent(Program.Resolve<AnimationDrawComponent>());
                    
                    newZone.Entities.Add(entity);
                }

                _zones.Add(newZone);
            }
        }

        public IZone GetZone(ZoneType zoneType) => _zones.First(x => x.ZoneType == zoneType);

        public void SetActiveZone(ZoneType zoneType)
        {
            _currentZone = zoneType;
            ZoneChanged?.Invoke(zoneType);
        }

        public void MoveEntityToZone(IZone oldZone, IZone zoneToGoTo, IEntity entity)
        {
            oldZone.RemoveEntity(entity);
            zoneToGoTo.AddEntity(entity);
        }

        public void Update(GameTime gameTime)
        {
            ActiveZone.Update(gameTime);
        }

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