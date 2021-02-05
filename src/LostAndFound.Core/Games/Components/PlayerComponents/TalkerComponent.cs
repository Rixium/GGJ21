using LostAndFound.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.Games.Components.PlayerComponents
{
    internal class TalkerComponent : Component
    {
        private readonly ZoneManager _zoneManager;
        private readonly IInputManager _inputManager;
        private bool _isInsideInteractionRange;

        private const int InteractionRange = 20;

        public TalkerComponent(ZoneManager zoneManager, IInputManager inputManager)
        {
            _zoneManager = zoneManager;
            _inputManager = inputManager;
        }
        
        public override void Start()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if(!_inputManager.KeyPressed(Keys.E))
            {
                return;
            }
            
            foreach (var entity in _zoneManager.ActiveZone.Entities)
            {
                _isInsideInteractionRange = Vector2.Distance(Entity.Position, entity.Position) < InteractionRange;

                if (!_isInsideInteractionRange) continue;
                
                var dialogComponent = entity.GetComponent<DialogComponent>();

                if (dialogComponent == null) continue;

                dialogComponent.Talk();
                break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}