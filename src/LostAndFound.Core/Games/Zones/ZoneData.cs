using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Zones
{
    public enum ZoneType
    {
        None,
        Test,
        Street,
        Forest
    }

    public class ZoneData
    {
        public ZoneType ZoneType { get; set; }
        public Collider[] Colliders { get; set; }
        public Texture2D BackgroundImage { get; set; }
    }
}