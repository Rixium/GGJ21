using System.Collections.Generic;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;
using NotImplementedException = System.NotImplementedException;

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
        public AnimalType AnimalType { get; set; }

        private IList<string> DialogHistory { get; } = new List<string>();

        public void AddDialogHistory(string text) => DialogHistory.Add(text);

        public IList<string> GetDialogHistory() => DialogHistory;
    }
}