using LostAndFound.Core.Games.Models;

namespace LostAndFound.Core.Games.Animals
{
    public interface IAnimalFactory
    {
        AnimalData Create();
    }
}