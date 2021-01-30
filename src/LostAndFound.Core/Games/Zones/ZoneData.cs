using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Zones
{
    public enum ZoneType
    {
        None,
        Street,
        Forest
    }

    public class ZoneData
    {
        public ZoneType ZoneType => ZoneType.Street;
        public Collider[] Colliders { get; set; }
        public Texture2D BackgroundImage { get; set; }
    }
}