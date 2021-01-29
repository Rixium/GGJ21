namespace LostAndFound.Core.Games.Models
{
    public enum ZoneType
    {
        Street
    }
    
    public class GameData
    {
        public ZoneType ActiveZone { get; set; } = ZoneType.Street;
        public PlayerData PlayerData { get; set; }
        public PersonData[] PersonData { get; set; }
    }
}