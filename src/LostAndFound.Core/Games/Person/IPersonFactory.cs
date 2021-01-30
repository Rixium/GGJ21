using LostAndFound.Core.Games.Models;

namespace LostAndFound.Core.Games.Person
{
    public interface IPersonFactory
    {
        public PersonData Create();
    }
}