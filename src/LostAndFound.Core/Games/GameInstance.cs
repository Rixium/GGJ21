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
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.Games
{
    class GameInstance : IGameInstance
    {
        private const ZoneType StartingZone = ZoneType.Forest;
        
        private Random _random = new Random();
        private GameInterface _gameInterface;

        private readonly IRenderManager _renderManager;
        private readonly IZoneLoader _zoneLoader;
        private readonly IContentChest _contentChest;
        private readonly IPersonFactory _personFactory;
        private readonly IAnimalFactory _animalFactory;
        private readonly Camera _camera;

        private GameData _gameData = new GameData();
        private IList<ZoneData> _zoneData;

        private ZoneData ActiveZone => _zoneData.First(x => x.ZoneType == _gameData.ActiveZone);
        private Player _player;

        public GameInstance(IRenderManager renderManager, IZoneLoader zoneLoader,
            IWindowConfiguration windowConfiguration, IContentChest contentChest, IPersonFactory personFactory,
            IAnimalFactory animalFactory, GameInterface gameInterface)
        {
            _renderManager = renderManager;
            _zoneLoader = zoneLoader;
            _contentChest = contentChest;
            _personFactory = personFactory;
            _animalFactory = animalFactory;
            _gameInterface = gameInterface;
            _camera = new Camera(windowConfiguration);
        }

        public void Load()
        {
            _zoneData = _zoneLoader.LoadZones();

            _gameData.ActiveZone = ZoneType.Forest;
            _camera.Position = new Vector2(500, 500);

            _personFactory.Load();
        }

        public void Start()
        {
            SetupGameData();

            var zoneColliders = ActiveZone.Colliders.ToList();
            var playerStartCollider = zoneColliders.First(x => x.Name.Equals("PlayerStart"));
            _gameData.PlayerData.Position = playerStartCollider.Bounds.ToVector2();

            _player = new Player(_contentChest.Get<Texture2D>("Images/Player/Idle_1"), _gameData);
            _camera.SetEntity(_gameData.PlayerData, true);

            _player.PlayerMove += CanPlayerMove;
            SpawnPerson();
            
            _gameInterface.SetUp(_gameData);
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
            
            _gameData.QuestData.Add(questData);
            _gameData.AnimalData.Add(animal);
            _gameData.PersonData.Add(person);
        }

        private AnimalData GetAnimalData() => _animalFactory.Create();

        public bool CanPlayerMove(Movement movement, Rectangle bounds)
        {
            var newBounds = new Rectangle(bounds.X + movement.X, bounds.Y + movement.Y, bounds.Width, bounds.Height);
            return ActiveZone.Colliders.Any(x => x.Bounds.Intersects(newBounds));
        }

        private void SetupGameData()
        {
            _gameData = new GameData
            {
                ActiveZone = StartingZone,
                PersonData = new List<PersonData>(),
                PlayerData = new PlayerData(),
                QuestData = new List<QuestData>(),
                AnimalData = new List<AnimalData>()
            };
        }

        public void AddMoney(int money)
        {
            _gameData.PlayerData.Cash += money;
            _gameInterface.OnMoneyChanged?.Invoke(_gameData.PlayerData.Cash);
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null,
                _camera.GetMatrix());

            _renderManager.SpriteBatch.Draw(ActiveZone.BackgroundImage, new Vector2(0, 0), Color.White);
            _renderManager.SpriteBatch.Draw(_player.Image, _gameData.PlayerData.Position, Color.White);

            foreach (var person in _gameData.PersonData)
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
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                AddMoney(10);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                SpawnPerson();
            }

            _camera.Update(20000, 20000);
            _player.Update(_gameData.PlayerData, gameTime);
            _camera.ToGo = _gameData.PlayerData.Position;
            _gameInterface.Update(gameTime);
        }
    }
}