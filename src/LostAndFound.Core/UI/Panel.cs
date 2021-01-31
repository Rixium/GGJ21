using System.Collections.Generic;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.UI
{
    public class Panel : IPanel
    {
        private readonly IRenderManager _renderManager;
        private readonly IList<IElement> _elements = new List<IElement>();
        private readonly IList<IElement> _elementsToRemove = new List<IElement>();

        public Panel(IRenderManager renderManager, string name)
        {
            _renderManager = renderManager;
            Name = name;
        }

        public string Name { get; }

        public void AddElement<T>(T element) where T : IElement
        {
            element.Panel = this;
            _elements.Add(element);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var element in _elementsToRemove)
            {
                _elements.Remove(element);
            }

            _elementsToRemove.Clear();

            foreach (var element in _elements)
            {
                element.Update(gameTime);

                // If the element needs to die (it would have been set by something)
                // then we add it to the elements to remove list for later.
                if (element.MarkedForDeath)
                {
                    _elementsToRemove.Add(element);
                }
            }
        }

        public void Draw()
        {
            foreach (var element in _elements)
            {
                element.Draw(_renderManager.SpriteBatch);
            }
        }
    }
}