using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games.Questing
{
    public class Quest
    {
        public string AnimalName { get; set; }
        public ZoneType AnimalZone { get; set; }
        public IEntity HandIn { get; set; }
        public bool Completed { get; set; }
        public Color AnimalColor { get; set; }
        public string AnimalImage { get; set; }
        public int Reward { get; set; }
    }
}