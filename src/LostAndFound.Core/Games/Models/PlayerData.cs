using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games.Models
{
    public class PlayerData
    {
        public string Name { get; set; }
        public int Cash { get; set; }
        public int Speed { get; set; }
        public Vector2 Position { get; set; }
        public List<string> AcceptedQuests { get; set; }
    }
}