using LostAndFound.Core.Config;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games
{
    public class Camera
    {
        private readonly IWindowConfiguration _windowConfiguration;
        private readonly ZoneManager _zoneManager;
        private Vector2 _position;

        private int _minX;
        private readonly int _maxX;
        private int _minY;
        private readonly int _maxY;

        private int _zoom = 3;

        private IEntity _following;

        public Camera(IWindowConfiguration windowConfiguration, ZoneManager zoneManager)
        {
            _windowConfiguration = windowConfiguration;
            _zoneManager = zoneManager;
            _position = Vector2.Zero;
            _maxX = 2000000;
            _maxY = 1000000;
        }

        public Vector2 ToGo { get; set; }

        public void Goto(Vector2 pos)
        {
            ToGo = pos;
        }

        public void Update(int maxX, int maxY)
        {
            maxX -= _windowConfiguration.WindowWidth / 2 / _zoom + 3;
            maxY = _zoneManager.ActiveZone.Image.Height - (_windowConfiguration.WindowHeight / 2 / _zoom + 3);
            _minX = _windowConfiguration.WindowWidth / 2 / _zoom + 1;
            _minY = _windowConfiguration.WindowHeight / 2 / _zoom;

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

            _position.X = MathHelper.Clamp(_position.X, _minX, maxX);
            _position.Y = MathHelper.Clamp(_position.Y, _minY, maxY);
        }

        public void SetEntity(IEntity entity, bool immediate)
        {
            _following = entity;

            if (immediate)
                Position = entity.Position;
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public Matrix GetMatrix() =>
            Matrix.CreateTranslation(new Vector3(-_position.X, -_position.Y, 0)) *
            Matrix.CreateScale(_zoom, _zoom, 1) *
            Matrix.CreateTranslation(new Vector3(_windowConfiguration.Center.X, _windowConfiguration.Center.Y, 0));

        public bool Intersects(Rectangle bounds) =>
            Bounds.Intersects(bounds);

        public Rectangle Bounds => new Rectangle((int) (_position.X - _windowConfiguration.Center.X),
            (int) (_position.Y - _windowConfiguration.Center.Y), _windowConfiguration.WindowWidth,
            _windowConfiguration.WindowHeight);
    }
}