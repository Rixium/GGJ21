namespace LostAndFound.Core.Games.Models
{
    public class QuestData
    {
        public PersonData PersonData { get; set; }
        public AnimalData AnimalData { get; set; }
        public int Reward { get; set; }
        public string[] ConversationData { get; set; }
    }
}