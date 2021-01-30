using LostAndFound.Core.Games.Zones;

namespace LostAndFound.Core.Games.Models
{
    public class GameData
    {
        public ZoneType ActiveZone { get; set; }
        public PlayerData PlayerData { get; set; }
        public PersonData[] PersonData { get; set; }
    }
}