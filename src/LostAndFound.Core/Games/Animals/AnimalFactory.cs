using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games.Animals
{
    public class AnimalFactory : IAnimalFactory
    {
        public AnimalData Create()
        {
            return new AnimalData
            {
                Name = "Pickle",
                Position = new Vector2(0, 0),
                CurrentZone = ZoneType.None
            };
        }
    }
}