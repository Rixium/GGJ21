using LostAndFound.Core.Games.Models;

namespace LostAndFound.Core.Persistence
{
    public interface IGameSaver
    {
        bool Save(string fileName, GameData gameData);
    }
}