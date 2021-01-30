using System.IO;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Persistence;
using LostAndFound.Core.System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Shouldly;

namespace LostAndFound.Core.IntegrationTests.Persistence
{
    public class GameSaverShould
    {
        private GameSaver _gameSaver;
        private ApplicationFolder _applicationFolder;
        private FileSystem _fileSystem;
        private string _testDirectory;

        [SetUp]
        public void SetUp()
        {
            _applicationFolder = new ApplicationFolder();
            _fileSystem = new FileSystem();
            _testDirectory = _applicationFolder.Create();
            _gameSaver = new GameSaver(_applicationFolder);
        }

        [TearDown]
        public void TearDown()
        {
            _fileSystem.Delete(_applicationFolder.Root);
        }

        [Test]
        public void SaveGameData()
        {
            var saveName = "testSave.xml";

            _gameSaver.Save(saveName, new GameData
            {
                ActiveZone = ZoneType.Street,
                PlayerData = new PlayerData
                {
                    Name = "Daniel",
                    Position = new Vector2(10, 10)
                }
            });

            _fileSystem.Exists(Path.Join(_testDirectory, saveName)).ShouldBeTrue();
        }
    }
}