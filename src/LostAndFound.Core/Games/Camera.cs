using LostAndFound.Core.Config;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games
{
    public class Camera
    {
        private const int Zoom = 4;

        private readonly IWindowConfiguration _windowConfiguration;
        private readonly ZoneManager _zoneManager;
        private Vector2 _position;

        private IEntity _following;

        public Camera(IWindowConfiguration windowConfiguration, ZoneManager zoneManager)
        {
            _windowConfiguration = windowConfiguration;
            _zoneManager = zoneManager;
            _position = Vector2.Zero;
        }

        private Vector2 ToGo { get; set; }

        private void Goto(Vector2 pos)
        {
            ToGo = pos;
        }

        public void Update()
        {
            var maxX = _zoneManager.ActiveZone.Image.Width - (_windowConfiguration.WindowWidth / 2 / Zoom + 3);
            var maxY = _zoneManager.ActiveZone.Image.Height - (_windowConfiguration.WindowHeight / 2 / Zoom + 3);
            var minX = _windowConfiguration.WindowWidth / 2 / Zoom + 1;
            var minY = _windowConfiguration.WindowHeight / 2 / Zoom;

            if (_following != null)
                Goto(new Vector2(_following.Position.X,
                    _following.Position.Y));

            var (x, y) = Vector2.Lerp(Position, ToGo, 0.03f);

            if (Vector2.Distance(Position, ToGo) < 10)
            {
                return;
            }

            _position.X = x;
            _position.Y = y;

            _position.X = MathHelper.Clamp(_position.X, minX, maxX);
            _position.Y = MathHelper.Clamp(_position.Y, minY, maxY);
        }

        public void OnZoneChanged(ZoneType zoneType)
        {
            var zoneSize = _zoneManager.ActiveZone.Image;
            _position = new Vector2(zoneSize.Width, zoneSize.Height) / 2f;
        }

        public void SetEntity(IEntity entity, bool immediate)
        {
            _following = entity;

            if (immediate)
                Position = entity.Position;
        }

        private Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public Matrix GetMatrix() =>
            Matrix.CreateTranslation(new Vector3(-_position.X, -_position.Y, 0)) *
            Matrix.CreateScale(Zoom, Zoom, 1) *
            Matrix.CreateTranslation(new Vector3(_windowConfiguration.Center.X, _windowConfiguration.Center.Y, 0));

        private Rectangle Bounds => new Rectangle((int) (_position.X - _windowConfiguration.Center.X),
            (int) (_position.Y - _windowConfiguration.Center.Y), _windowConfiguration.WindowWidth,
            _windowConfiguration.WindowHeight);
    }
}