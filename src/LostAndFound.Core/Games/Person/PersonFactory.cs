using System.Collections.Generic;
using System.IO;
using LostAndFound.Core.Content;
using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Person
{
    public class PersonFactory : IPersonFactory
    {
        private IContentChest _contentChest;
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
            return new PersonData
            {
                Name = "Daniel"
            };
        }
    }
}