using System;
using System.Collections.Generic;

namespace Finate.UWP.Band
{
    public interface ICustomBandTile
    {
        string Name { get; }

        Dictionary<Guid, dynamic> Layouts { get; }

        Guid Guid { get; }

        string LargeIconPath { get; }

        string SmallIconPath { get; }
    }
}