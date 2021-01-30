using System;

namespace LostAndFound.Core.Games.Models
{
    public class QuestData
    {
        public string Id = Guid.NewGuid().ToString();
        public string PersonId { get; set; }
        public string AnimalId { get; set; }
        public int Reward { get; set; }
        public string[] ConversationData { get; set; }
    }
}