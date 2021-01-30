using LostAndFound.Core.Games.Models;
using LostAndFound.Core.System;

namespace LostAndFound.Core.Persistence
{
    public class GameSaver : IGameSaver
    {
        private readonly IApplicationFolder _applicationFolder;

        public GameSaver(IApplicationFolder applicationFolder)
        {
            _applicationFolder = applicationFolder;
        }

        public bool Save(string fileName, GameData gameData)
        {
            _applicationFolder.Save(fileName, gameData, true);
            return true;
        }
    }
}