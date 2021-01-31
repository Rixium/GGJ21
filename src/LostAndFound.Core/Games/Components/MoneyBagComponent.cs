using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class MoneyBagComponent : IComponent
    {
        public IEntity Entity { get; set; }

        public int Money { get; private set; }

        public void Start()
        {
        }

        public void AddMoney(int money) => Money += money;

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}