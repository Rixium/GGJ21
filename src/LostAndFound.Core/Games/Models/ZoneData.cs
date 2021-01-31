using LostAndFound.Core.Games.Zones;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Models
{
    public enum ZoneType
    {
        None,
        Street,
        Forest,
        Park
    }

    public class ZoneData
    {
        public ZoneType ZoneType { get; set; }
        public Collider[] Colliders { get; set; }
        public Texture2D BackgroundImage { get; set; }
    }
}