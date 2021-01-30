using System.Collections.Generic;
using LostAndFound.Core.Games.Models;

namespace LostAndFound.Core.Games.Zones
{
    public interface IZoneLoader
    {
        IList<ZoneData> LoadZones();
    }
}