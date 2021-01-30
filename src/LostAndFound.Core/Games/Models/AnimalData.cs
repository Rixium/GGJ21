using System;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games.Models
{
    public class AnimalData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public ZoneType CurrentZone { get; set; } = ZoneType.None;
        public string Name { get; set; }
        public Vector2 Position { get; set; }
    }
}