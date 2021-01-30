using LostAndFound.Core.Games.Models;

namespace LostAndFound.Core.Games.Person
{
    public interface IPersonFactory
    {

        void Load();
        public PersonData Create();
    }
}