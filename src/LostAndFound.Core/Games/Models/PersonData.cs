using System;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games.Models
{
    public class PersonData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string ImageName { get; set; }
        public Vector2 Position { get; set; }
    }
}