using System;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Input;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games.Services
{
    public interface IService
    {
        public IGameInstance GameInstance { get; set; }
        void Start();
        void Update(GameTime gameTime);
    }

    public class PersonInteractionService : IService
    {
        private readonly IInputManager _inputManager;

        public PersonInteractionService(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        public IGameInstance GameInstance { get; set; }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (GameInstance.GameData.ActiveZone == ZoneType.Street)
            {
                foreach (var person in GameInstance.GameData.PersonData)
                {
                    var distanceToPerson = Vector2.Distance(GameInstance.GameData.PlayerData.Position, person.Position);

                    if (distanceToPerson < 20)
                    {
                        Console.WriteLine("You're standing next to " + person.Name);
                        break;
                    }
                }
            }
        }
    }
}