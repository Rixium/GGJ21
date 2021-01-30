using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games.Zones
{
    public class ZoneLoader : IZoneLoader
    {
        private readonly IContentLoader<AsepriteSpriteMap> _asepriteSpriteMapLoader;

        public ZoneLoader(IContentLoader<AsepriteSpriteMap> asepriteSpriteMapLoader)
        {
            _asepriteSpriteMapLoader = asepriteSpriteMapLoader;
        }

        public IList<ZoneData> LoadZones()
        {
            var zonePaths = Directory.EnumerateFiles("Assets/Data/Regions", "*.json").ToList();
            var zoneData = new List<ZoneData>();

            foreach (var zonePath in zonePaths)
            {
                var spriteMap = _asepriteSpriteMapLoader.GetContent(zonePath);
                var zoneName = Path.GetFileNameWithoutExtension(zonePath);
                Enum.TryParse<ZoneType>(zoneName, out var zoneType);

                zoneData.Add(new ZoneData
                {
                    ZoneType = zoneType,
                    BackgroundImage = spriteMap.Image,
                    Colliders = spriteMap.Regions.Select(CreateColliderFromRegion)
                        .ToArray()
                });
            }

            return zoneData;
        }

        private static Collider CreateColliderFromRegion(AsepriteSlice spriteMapRegion)
        {
            var propertyDictionary = CreatePropertyDictionary(spriteMapRegion);
            var colliderBounds = spriteMapRegion.Keys.FirstOrDefault()?.Bounds;
            return new Collider
            {
                Name = spriteMapRegion.Name ?? "Unnamed_Region",
                Bounds = colliderBounds != null
                    ? new Rectangle(colliderBounds.X, colliderBounds.Y, colliderBounds.W, colliderBounds.H)
                    : new Rectangle(0, 0, 0, 0),
                Properties = propertyDictionary
            };
        }

        private static Dictionary<string, string> CreatePropertyDictionary(AsepriteSlice spriteMapRegion)
        {
            var propertySets =
                spriteMapRegion.Data?.Split(new[] {'(', ')'}, StringSplitOptions.RemoveEmptyEntries);
            return propertySets?
                .Select(item => item.Split(':'))
                .ToDictionary(s => s[0], s => s[1]);
        }
    }
}