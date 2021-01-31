using LostAndFound.Core.Games.Entities;

namespace LostAndFound.Core.Games.Questing
{
    public class Quest
    {
        public string AnimalName { get; set; }
        public IEntity HandIn { get; set; }
        public bool Completed { get; set; }
    }
}