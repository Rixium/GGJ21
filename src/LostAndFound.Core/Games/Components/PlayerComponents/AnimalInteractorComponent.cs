using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components.PlayerComponents
{
    internal class AnimalInteractorComponent : Component
    {
        private readonly ZoneManager _zoneManager;
        private bool _isInsideInteractionRange;
        private const int InteractionRange = 50;
        
        public AnimalInteractorComponent(ZoneManager zoneManager)
        {
            _zoneManager = zoneManager;
        }
        
        public override void Start()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in _zoneManager.ActiveZone.Entities)
            {
                if (entity == Entity)
                {
                    continue;
                }
                
                _isInsideInteractionRange = (Vector2.Distance(Entity.Position, entity.Position) < InteractionRange);
                
                if (!_isInsideInteractionRange)
                {
                    continue;
                }
                
                var animalComponent = entity.GetComponent<AnimalComponent>();

                if (animalComponent == null)
                {
                    continue;
                }
                
                animalComponent.Follow(Entity);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}