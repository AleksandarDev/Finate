using System;
using System.Collections.Generic;
using Finate.UWP.Band.Layouts;

namespace Finate.UWP.Band.Tiles
{
    public class TransactionsBandTile : ICustomBandTile
    {
        // GUIDs
        public const string TileGuid = "FAD0868B-C694-48EC-8561-0E053B8026D0";
        public const string TransactionsPageGuid = "FAD0868B-C694-48EC-8561-0E053B803100";
        public const string TransactionsConfirmPageGuid = "FAD0868B-C694-48EC-8561-0E053B803200";

        // Constants
        private const string TileName = "Finate Transactions";
        private const string TileLargeIconPath = "ms-appx:///Assets/BandTileIconLarge.png";
        private const string TileSmallIconPath = "ms-appx:///Assets/BandTileIconSmall.png";


        public string Name => TileName;

        public Dictionary<Guid, dynamic> Layouts { get; } = new Dictionary<Guid, dynamic>
        {
            {new Guid(TransactionsPageGuid), new TransactionTileLayout()},
            {new Guid(TransactionsConfirmPageGuid), new TransactionConfirmTileLayout()}
        };

        public Guid Guid => new Guid(TileGuid);

        public string LargeIconPath => TileLargeIconPath;

        public string SmallIconPath => TileSmallIconPath;
    }
}