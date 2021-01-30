using LostAndFound.Core.Games.Models;

namespace LostAndFound.Core.Games.Person
{
    public class PersonFactory : IPersonFactory
    {
        public PersonData Create()
        {
            return new PersonData
            {
                Name = "Daniel"
            };
        }
    }
}