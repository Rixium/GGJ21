using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games
{
    public interface IGameInstance
    {
        public IZone ActiveZone { get; }
        void Load();
        void Start();
        void Draw();
        void Update(GameTime gameTime);
        IZone GetZone(ZoneType zoneType);
        void SetActiveZone(ZoneType zoneType);
        void MoveEntityToZone(IZone oldZone, IZone zoneToGoTo, IEntity entity);
    }
}