using System;
using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Animals;
using LostAndFound.Core.Games.Interfaces;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Person;
using LostAndFound.Core.Games.Services;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    public class GameInstance : IGameInstance
    {
        private const ZoneType StartingZone = ZoneType.Street;

        private readonly Random _random = new Random();
        private readonly GameInterface _gameInterface;
        private readonly IReadOnlyCollection<IService> _services;
        private readonly IRenderManager _renderManager;
        private readonly IZoneLoader _zoneLoader;
        private readonly IContentChest _contentChest;
        private readonly IPersonFactory _personFactory;
        private readonly IAnimalFactory _animalFactory;
        private readonly Camera _camera;

        public GameData GameData { get; private set; } = new GameData();
        private IList<ZoneData> _zoneData;

        private ZoneData ActiveZone => _zoneData.First(x => x.ZoneType == GameData.ActiveZone);
        private Player _player;

        public GameInstance(IRenderManager renderManager, IZoneLoader zoneLoader,
            IWindowConfiguration windowConfiguration, IContentChest contentChest, IPersonFactory personFactory,
            IAnimalFactory animalFactory, GameInterface gameInterface, IReadOnlyCollection<IService> services)
        {
            _renderManager = renderManager;
            _zoneLoader = zoneLoader;
            _contentChest = contentChest;
            _personFactory = personFactory;
            _animalFactory = animalFactory;
            _gameInterface = gameInterface;
            _services = services;

            _camera = new Camera(windowConfiguration);
        }

        public void Load()
        {
            _zoneData = _zoneLoader.LoadZones();

            GameData.ActiveZone = ZoneType.Forest;

            _personFactory.Load();

            foreach (var service in _services)
                service.GameInstance = this;
        }

        public void Start()
        {
            SetupGameData();

            var zoneColliders = ActiveZone.Colliders.ToList();
            var playerStartCollider = zoneColliders.First(x => x.Name.Equals("PlayerStart"));
            GameData.PlayerData.Position = playerStartCollider.Bounds.ToVector2();

            _player = new Player(_contentChest.Get<Texture2D>("Images/Player/Idle_1"), GameData);
            _camera.SetEntity(GameData.PlayerData, false);

            _player.PlayerMove += CanPlayerMove;
            SpawnPerson();

            _gameInterface.SetUp(GameData);
            
            foreach (var service in _services)
            {
                service.GameInstance = this;
                service.Start();
            }
        }

        public void SpawnPerson()
        {
            var person = _personFactory.Create();
            var safeZone = ActiveZone.Colliders.First(x => x.Name.Equals("SafeZone"));
            person.Position = new Vector2(_random.Next(safeZone.Bounds.X, safeZone.Bounds.Right),
                _random.Next(safeZone.Bounds.Y, safeZone.Bounds.Bottom));

            var animal = GetAnimalData();

            var questData = new QuestData
            {
                PersonId = person.Id,
                AnimalId = animal.Id,
                Reward = 1000,
                ConversationData = new[] {"Hello Son!", "Lost me dog!"}
            };

            GameData.QuestData.Add(questData);
            GameData.AnimalData.Add(animal);
            GameData.PersonData.Add(person);
        }

        private AnimalData GetAnimalData() => _animalFactory.Create();

        public bool CanPlayerMove(Movement movement, Rectangle bounds)
        {
            var newBounds = new Rectangle(bounds.X + movement.X, bounds.Y + movement.Y, bounds.Width, bounds.Height);
            return ActiveZone.Colliders.Any(x => x.Bounds.Intersects(newBounds));
        }

        private void SetupGameData()
        {
            GameData = new GameData
            {
                ActiveZone = StartingZone,
                PersonData = new List<PersonData>(),
                PlayerData = new PlayerData(),
                QuestData = new List<QuestData>(),
                AnimalData = new List<AnimalData>()
            };
        }

        public void AddMoney(int money) => GameData.PlayerData.Cash += money;

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null,
                _camera.GetMatrix());

            _renderManager.SpriteBatch.Draw(ActiveZone.BackgroundImage, new Vector2(0, 0), Color.White);
            _renderManager.SpriteBatch.Draw(_player.Image, GameData.PlayerData.Position, Color.White);

            foreach (var person in GameData.PersonData)
            {
                var personImage = _contentChest.Get<Texture2D>(person.ImageName);
                _renderManager.SpriteBatch.Draw(personImage, person.Position, Color.White);
            }

            _renderManager.SpriteBatch.End();

            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            _gameInterface.Draw();
            _renderManager.SpriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            _camera.Update(500, 281);
            _player.Update(GameData.PlayerData, gameTime);
            _camera.ToGo = GameData.PlayerData.Position;
            _gameInterface.Update(gameTime);
            
            foreach(var service in _services)
                service.Update(gameTime);
        }
    }
}