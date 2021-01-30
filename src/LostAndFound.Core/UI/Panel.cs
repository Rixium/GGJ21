using System.Collections.Generic;
using LostAndFound.Core.Graphics;

namespace LostAndFound.Core.UI
{
    public class Panel : IPanel
    {
        private readonly IRenderManager _renderManager;
        private readonly IList<IElement> _elements = new List<IElement>();

        public Panel(IRenderManager renderManager)
        {
            _renderManager = renderManager;
        }

        public void AddElement<T>(T element) where T : IElement
        {
            element.Panel = this;
            _elements.Add(element);
        }

        public void Update()
        {
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