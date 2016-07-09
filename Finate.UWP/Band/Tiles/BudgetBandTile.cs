using System;
using System.Collections.Generic;
using Finate.UWP.Band.Layouts;

namespace Finate.UWP.Band.Tiles
{
    /// <summary>
    /// The budget band tile.
    /// </summary>
    /// <seealso cref="Finate.UWP.Band.ICustomBandTile" />
    public class BudgetBandTile : ICustomBandTile
    {
        /// <summary>
        /// The tile unique identifier.
        /// </summary>
        public const string TileGuid = "FAD0868B-C694-48EC-8561-0E053B8126D0";

        /// <summary>
        /// The overview page unique identifier.
        /// </summary>
        public const string OverviewPageGuid = "FAD0868B-C694-48EC-8561-0E053B813100";

        // Constants
        private const string TileName = "Finate Budget";
        private const string TileLargeIconPath = "ms-appx:///Assets/BandTileIconLarge.png";
        private const string TileSmallIconPath = "ms-appx:///Assets/BandTileIconSmall.png";


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => TileName;

        /// <summary>
        /// Gets the layouts.
        /// </summary>
        /// <value>
        /// The layouts.
        /// </value>
        public Dictionary<Guid, dynamic> Layouts { get; } = new Dictionary<Guid, dynamic>
        {
            {new Guid(OverviewPageGuid), new BudgetOverviewTileLayout()}
        };

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public Guid Guid => new Guid(TileGuid);

        /// <summary>
        /// Gets the large icon path.
        /// </summary>
        /// <value>
        /// The large icon path.
        /// </value>
        public string LargeIconPath => TileLargeIconPath;

        /// <summary>
        /// Gets the small icon path.
        /// </summary>
        /// <value>
        /// The small icon path.
        /// </value>
        public string SmallIconPath => TileSmallIconPath;
    }
}