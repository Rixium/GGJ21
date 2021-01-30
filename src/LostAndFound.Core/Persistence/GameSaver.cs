using LostAndFound.Core.Games.Models;
using LostAndFound.Core.System;
using Newtonsoft.Json;

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
            var json = JsonConvert.SerializeObject(gameData);
            _applicationFolder.Save(fileName, json, true);
            return true;
        }
    }
}