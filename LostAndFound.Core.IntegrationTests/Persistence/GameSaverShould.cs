using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Persistence;
using LostAndFound.Core.System;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace LostAndFound.Core.IntegrationTests.Persistence
{
    public class GameSaverShould
    {
        private GameSaver _gameSaver;

        [SetUp]
        public void SetUp()
        {
            var applicationFolder = new ApplicationFolder();
            _gameSaver = new GameSaver(applicationFolder);
        }

        [SetUp]
        public void SaveGameData()
        {
            _gameSaver.Save("TestFile", new GameData
            {
                ActiveZone = ZoneType.Street,
                PlayerData = new PlayerData
                {
                    Name = "Daniel",
                    Position = new Vector2(10, 10)
                }
            });
        }
    }
}