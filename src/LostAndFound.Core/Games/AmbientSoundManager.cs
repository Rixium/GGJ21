using System;
using LostAndFound.Core.Content;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    public class AmbientSoundManager : ISystem
    {
        private readonly ZoneManager _zoneManager;
        private readonly IContentChest _contentChest;

        private SoundEffectInstance _ambientInstance;
        private SoundEffect _streetAmbientSound;
        private SoundEffect _forestAmbientSound;

        public AmbientSoundManager(ZoneManager zoneManager, IContentChest contentChest)
        {
            _zoneManager = zoneManager;
            _contentChest = contentChest;
            zoneManager.ZoneChanged += OnZoneChanged;
        }

        void OnZoneChanged(ZoneType zoneType)
        {
            _ambientInstance.Stop();
            switch (zoneType)
            {
                case ZoneType.None:
                    break;
                case ZoneType.Test:
                    break;
                case ZoneType.Street:
                    _ambientInstance = _streetAmbientSound.CreateInstance();
                    break;
                case ZoneType.Forest:
                    _ambientInstance = _forestAmbientSound.CreateInstance();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(zoneType), zoneType, null);
            }
            _ambientInstance.Play();
        }

        public void Start()
        {
            _streetAmbientSound = _contentChest.Get<SoundEffect>("Audio/Ambience/StreetAmbience");
            _forestAmbientSound = _contentChest.Get<SoundEffect>("Audio/Ambience/ForestAmbience");
            _ambientInstance = _streetAmbientSound.CreateInstance();
            _ambientInstance.IsLooped = true;
            _ambientInstance.Play();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}