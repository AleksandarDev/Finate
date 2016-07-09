using System;
using System.Collections.Generic;
using Finate.UWP.Band.Layouts;

namespace Finate.UWP.Band.Tiles
{
    public class BudgetBandTile : ICustomBandTile
    {
        // GUIDs
        public const string TileGuid = "FAD0868B-C694-48EC-8561-0E053B8126D0";
        public const string OverviewPageGuid = "FAD0868B-C694-48EC-8561-0E053B813100";

        // Constants
        private const string TileName = "Finate Budget";
        private const string TileLargeIconPath = "ms-appx:///Assets/BandTileIconLarge.png";
        private const string TileSmallIconPath = "ms-appx:///Assets/BandTileIconSmall.png";

        public string Name => TileName;

        public Dictionary<Guid, dynamic> Layouts { get; } = new Dictionary<Guid, dynamic>
        {
            {new Guid(OverviewPageGuid), new BudgetOverviewTileLayout()}
        };

        public Guid Guid => new Guid(TileGuid);

        public string LargeIconPath => TileLargeIconPath;

        public string SmallIconPath => TileSmallIconPath;
    }
}