using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Finate.UWP.Annotations;
using Microsoft.Band;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using Serilog;

namespace Finate.UWP.Band
{
    public abstract class BandTileManager
    {
        private readonly ILogger logger;
        private readonly IBandConnections bandConnections;


        protected BandTileManager(
            [NotNull] ILogger logger,
            [NotNull] IBandConnections bandConnections)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (bandConnections == null) throw new ArgumentNullException(nameof(bandConnections));

            this.logger = logger;
            this.bandConnections = bandConnections;
        }

        protected async Task<bool> IsTileInstalledAsync(Guid tileId)
        {
            try
            {
                var isOwned = await Task.Run(() =>
                    this.bandConnections.BandClient.TileManager.TileInstalledAndOwned(
                        ref tileId,
                        CancellationToken.None));

                return isOwned;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Failed to retrieve band {TileGUID} tile installed state.", tileId.ToString());
                return false;
            }
        }

        protected async Task<bool> IsTileInstalledAsync(string tileGuid)
        {
            return await this.IsTileInstalledAsync(new Guid(tileGuid));
        }

        protected async Task CreateCustomBandTileAsync(ICustomBandTile tileDefinition)
        {
            try
            {
                // Construct tile
                var tile = new BandTile(tileDefinition.Guid)
                {
                    Name = tileDefinition.Name,
                    TileIcon = await LoadIconAsync(tileDefinition.LargeIconPath),
                    SmallIcon = await LoadIconAsync(tileDefinition.SmallIconPath)
                };

                // Construct layouts
                var indexCounter = 0;
                var layoutIndexes = new Dictionary<Guid, int>();
                foreach (var layoutDefinition in tileDefinition.Layouts)
                {
                    // Add to tile page layouts
                    tile.PageLayouts.Add(layoutDefinition.Value.Layout);

                    // Load icons
                    await layoutDefinition.Value.LoadIconsAsync(tile);

                    // Assign index
                    layoutIndexes.Add(layoutDefinition.Key, indexCounter++);
                }

                // Remove existing tile
                if (await this.IsTileInstalledAsync(tileDefinition.Guid))
                    await this.bandConnections.BandClient.TileManager.RemoveTileAsync(tileDefinition.Guid);

                // Add tile 
                await this.bandConnections.BandClient.TileManager.AddTileAsync(tile);

                // Populate with default data
                await this.bandConnections.BandClient.TileManager.SetPagesAsync(
                    tileDefinition.Guid,
                    tileDefinition.Layouts.Select(layoutDefinition => new PageData(
                        layoutDefinition.Key, 
                        layoutIndexes[layoutDefinition.Key], 
                        layoutDefinition.Value.Data.All)));
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Failed to create tile.");
            }
        }

        protected static async Task<BandIcon> LoadIconAsync(string path)
        {
            var imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(path));
            using (var stream = await imageFile.OpenAsync(FileAccessMode.Read))
            {
                var bitmap = new WriteableBitmap(1, 1);
                await bitmap.SetSourceAsync(stream);
                return bitmap.ToBandIcon();
            }
        }
    }
}
