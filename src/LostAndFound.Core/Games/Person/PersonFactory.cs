using System;
using System.Collections.Generic;
using System.IO;
using Asepreadr;
using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Person
{
    public class PersonFactory : IPersonFactory
    {
        private readonly Random _random = new Random();
        private readonly IContentChest _contentChest;
        
        private List<Texture2D> _personTextures;

        public PersonFactory(IContentChest contentChest)
        {
            _contentChest = contentChest;
        }

        public void Load()
        {
            _personTextures = new List<Texture2D>();
            foreach (var person in Directory.GetFiles("Assets\\Images\\People"))
            {
                var personTexture = _contentChest.Get<Texture2D>(person);
                _personTextures.Add(personTexture);
            }
            
        }
        
        public PersonData Create()
        {
            var selectedPerson = _random.Next(0, _personTextures.Count);
            var randomPerson = _personTextures[selectedPerson];
            var randomName = GetRandomName();
            
            return new PersonData
            {
                Name = randomName,
                ImageName = randomPerson.Name
            };
        }

        private string GetRandomName() => "Daniel";
    }
}