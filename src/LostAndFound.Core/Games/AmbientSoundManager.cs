﻿using System;
using Asepreadr;
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
        private SoundEffect _parkAmbientSound;

        public AmbientSoundManager(ZoneManager zoneManager, IContentChest contentChest)
        {
            _zoneManager = zoneManager;
            _contentChest = contentChest;
        }

        void OnZoneChanged(ZoneType zoneType)
        {
            _ambientInstance.Stop();
            switch (zoneType)
            {
                case ZoneType.None:
                    break;
                case ZoneType.Street:
                    _ambientInstance = _streetAmbientSound.CreateInstance();
                    _ambientInstance.Volume = 0.5f;
                    break;
                case ZoneType.Forest:
                    _ambientInstance = _forestAmbientSound.CreateInstance();
                    _ambientInstance.Volume = 0.3f;
                    break;
                case ZoneType.Park:
                    _ambientInstance = _parkAmbientSound.CreateInstance();
                    _ambientInstance.Volume = 0.6f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(zoneType), zoneType, null);
            }
            _ambientInstance.IsLooped = true;
            _ambientInstance.Play();
        }

        public void Start()
        {
            _streetAmbientSound = _contentChest.Get<SoundEffect>("Audio/Ambience/StreetAmbience");
            _forestAmbientSound = _contentChest.Get<SoundEffect>("Audio/Ambience/ForestAmbience");
            _parkAmbientSound = _contentChest.Get<SoundEffect>("Audio/Ambience/ParkAmbience");
            _ambientInstance = _streetAmbientSound.CreateInstance();
            _ambientInstance.IsLooped = true;
            _ambientInstance.Play();
            
            _zoneManager.ZoneChanged += OnZoneChanged;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}