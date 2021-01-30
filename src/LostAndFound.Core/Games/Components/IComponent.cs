using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public interface IComponent
    {
        IEntity Entity { get; set; }
        void Start();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}