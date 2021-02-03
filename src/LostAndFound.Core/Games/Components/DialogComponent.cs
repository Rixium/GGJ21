using System;
using System.Collections.Generic;
using Asepreadr;
using Asepreadr.Aseprite;
using Asepreadr.Graphics;
using Asepreadr.Loaders;
using LostAndFound.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    internal class DialogComponent : Component
    {
        private SoundEffectInstance _soundEffect;
        private readonly IContentChest _contentChest;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        private readonly Queue<string> _dialogQueue = new Queue<string>();

        private Random _random = new Random();
        private string _activeDialog;
        private SpriteFont _dialogFont;
        private Texture2D _pixel;
        private Sprite _nextButtonSprite;
        private int _curr;
        private float _timer;
        private WandererComponent _wandererComponent;
        private Queue<char> _letterQueue = new Queue<char>();

        public DialogComponent(IContentChest contentChest, IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _contentChest = contentChest;
            _spriteMapLoader = spriteMapLoader;
        }

        public override void Start()
        {
            var spriteMap = _spriteMapLoader.GetContent("Assets\\UI\\UI.json");
            _nextButtonSprite = spriteMap.CreateSpriteFromRegion("Next_Button");

            _dialogFont = _contentChest.Get<SpriteFont>("Fonts\\dialogFont");
            _pixel = _contentChest.Get<Texture2D>("Utils\\pixel");
            _wandererComponent = Entity.GetComponent<WandererComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            if (_letterQueue.Count > 0)
            {
                if (_soundEffect == null || _soundEffect.State == SoundState.Stopped)
                {
                    var letter = _letterQueue.Dequeue();
                    var sound = _contentChest.Get<SoundEffect>($"Audio\\SoundEffects\\Mouth\\{letter}");
                    _soundEffect = sound.CreateInstance();
                    _soundEffect.Pitch = (float) (_random.NextDouble() * (1 - -1) - 1);
                    _soundEffect.Play();
                }
            }

            if (_activeDialog == null || _curr == _activeDialog.Length)
            {
                return;
            }

            _timer -= gameTime.AsDelta();

            if (_timer > 0)
            {
                return;
            }

            _timer = 0.05f;
            _curr++;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_activeDialog == null) return;
            var dialogPosition = GetDialogPosition();
            var dialogSize = _dialogFont.MeasureString(_activeDialog);
            var scaledDialogSize = _dialogFont.MeasureString(_activeDialog) * 0.5f;
            var backSize = new Rectangle((int) dialogPosition.X, (int) dialogPosition.Y,
                (int) (scaledDialogSize.X + 5),
                (int) (scaledDialogSize.Y + 5));

            spriteBatch.Draw(_pixel, backSize, null, Color.Black * 0.8f, 0f, Vector2.One * 0.5f, SpriteEffects.None,
                0f);
            DrawOutline(spriteBatch, backSize, Color.White);
            spriteBatch.DrawString(_dialogFont, _activeDialog.Substring(0, _curr), dialogPosition, Color.White, 0f,
                dialogSize * 0.5f,
                0.5f, SpriteEffects.None, 0f);

            spriteBatch.Draw(_nextButtonSprite.Texture,
                new Vector2(backSize.X + backSize.Width / 2f + 5, backSize.Y - _nextButtonSprite.Height / 2f),
                _nextButtonSprite.Source, Color.White, 0f, _nextButtonSprite.Origin, 1f, SpriteEffects.None, 0f);
        }

        private void DrawOutline(SpriteBatch spriteBatch, Rectangle backSize, Color color)
        {
            spriteBatch.Draw(_pixel,
                new Rectangle((int) (backSize.X - backSize.Width / 2f), (int) (backSize.Y - (backSize.Height / 2f)),
                    backSize.Width, 1), color);
            spriteBatch.Draw(_pixel,
                new Rectangle((int) (backSize.X - backSize.Width / 2f), (int) (backSize.Y + (backSize.Height / 2f) - 1),
                    backSize.Width, 1), color);
            spriteBatch.Draw(_pixel,
                new Rectangle((int) (backSize.X - backSize.Width / 2f), (int) (backSize.Y - (backSize.Height / 2f)), 1,
                    backSize.Height), color);
            spriteBatch.Draw(_pixel,
                new Rectangle((int) (backSize.X + backSize.Width / 2f - 1), (int) (backSize.Y - (backSize.Height / 2f)),
                    1, backSize.Height), color);
        }

        private Vector2 GetDialogPosition()
        {
            var position = Entity.Position + new Vector2(Entity.Width / 2f, -10);
            return position;
        }

        public string AddText(string text)
        {
            _dialogQueue.Enqueue(text);
            return text;
        }

        public bool Talk()
        {
            if (!HasDialog())
            {
                _activeDialog = null;
                return false;
            }

            _activeDialog = GetNextDialog();

            _letterQueue = new Queue<char>();
            foreach (var letter in _activeDialog.ToLower())
            {
                if (char.IsLetter(letter))
                {
                    _letterQueue.Enqueue(letter);
                }
            }

            _curr = 0;

            return true;
        }

        public bool HasDialog() => _dialogQueue.Count > 0;

        public string GetNextDialog() => _dialogQueue.Dequeue();

        public void SetNear(bool near) => _wandererComponent.Active = !near;
    }
}