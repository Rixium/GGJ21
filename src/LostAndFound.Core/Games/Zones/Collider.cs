using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games.Zones
{
    public class Collider
    {
        public string Name { get; set; }
        public Rectangle Bounds { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public string GetProperty(string propertyName)
        {
            if (Properties == null)
            {
                return null;
            }

            Properties.TryGetValue(propertyName, out var propertyValue);
            return propertyValue;
        }
    }
}