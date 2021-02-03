using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class MoneyBagComponent : Component
    {
        

        public int Money { get; private set; }

        public override void Start()
        {
        }

        public void AddMoney(int money) => Money += money;

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}