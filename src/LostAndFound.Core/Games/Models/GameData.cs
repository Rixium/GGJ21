using LostAndFound.Core.Games.Zones;

namespace LostAndFound.Core.Games.Models
{
    public class GameData
    {
        public ZoneType ActiveZone { get; set; } = ZoneType.Street;
        public PlayerData PlayerData { get; set; }
        public PersonData[] PersonData { get; set; }
    }
}