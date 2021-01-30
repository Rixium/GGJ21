using System.Collections.Generic;
using LostAndFound.Core.Games.Zones;

namespace LostAndFound.Core.Games.Models
{
    public class GameData
    {
        public ZoneType ActiveZone { get; set; }
        public PlayerData PlayerData { get; set; }
        public List<PersonData> PersonData { get; set; }

        public List<QuestData> QuestData { get; set; }
        public List<AnimalData> AnimalData { get; set; }
    }
}