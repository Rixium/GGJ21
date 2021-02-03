using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Questing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class QuestFulfilmentComponent : Component
    {
        
        public Quest Quest { get; set; }
        public IEntity QuestFulfiller { get; set; }

        public override void Start()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}