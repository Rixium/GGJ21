using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NotImplementedException = System.NotImplementedException;

namespace LostAndFound.Core.Games
{
    public class AmbientSoundManager : ISystem
    {
        private readonly ZoneManager _zoneManager;

        public AmbientSoundManager(ZoneManager zoneManager)
        {
            _zoneManager = zoneManager;
            zoneManager.ZoneChanged += OnZoneChanged;
        }

        void OnZoneChanged(ZoneType zoneType)
        {
            
        }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}