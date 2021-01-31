using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Systems
{
    public interface ISystem
    {
        void Start();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}