using System.Collections.Generic;

namespace LostAndFound.Core.Games.Zones
{
    public interface IZoneLoader
    {
        IList<ZoneData> LoadZones();
    }
}