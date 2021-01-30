using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games.Zones
{
    public class Collider
    {
        public string Name { get; set; }
        public Rectangle Bounds { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}