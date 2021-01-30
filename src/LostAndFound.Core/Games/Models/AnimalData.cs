using LostAndFound.Core.Games.Zones;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games.Models
{
    public class AnimalData
    {
        public ZoneType CurrentZone { get; set; } = ZoneType.None;
        public string Name { get; set; }
        public Vector2 Position { get; set; }
    }
}