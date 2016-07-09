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
    /// <summary>
    /// The generic band tile manager.
    /// </summary>
    public abstract class BandTileManagerBase
    {
        private readonly ILogger logger;
        private readonly IBandConnectionsManager bandConnectionsManager;


        /// <summary>
        /// Initializes a new instance of the <see cref="BandTileManagerBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="bandConnectionsManager">The band connections manager.</param>
        /// <exception cref="ArgumentNullException">
        /// logger
        /// or
        /// bandConnectionsManager
        /// </exception>
        protected BandTileManagerBase(
            [NotNull] ILogger logger,
            [NotNull] IBandConnectionsManager bandConnectionsManager)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (bandConnectionsManager == null) throw new ArgumentNullException(nameof(bandConnectionsManager));

            this.logger = logger;
            this.bandConnectionsManager = bandConnectionsManager;
        }


        /// <summary>
        /// Determines whether the specified tile is installed.
        /// </summary>
        /// <param name="tileId">The tile identifier.</param>
        /// <returns>Returns <c>True</c> if tile with specified identifier is installed; <c>False</c> otherwise.</returns>
        protected virtual async Task<bool> IsTileInstalledAsync(Guid tileId)
        {
            try
            {
                var isOwned = await Task.Run(() =>
                    this.bandConnectionsManager.BandClient.TileManager.TileInstalledAndOwned(
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

        /// <summary>
        /// Determines whether the specified tile is installed.
        /// </summary>
        /// <param name="tileGuid">The tile unique identifier.</param>
        /// <returns>Returns <c>True</c> if tile with specified identifier is installed; <c>False</c> otherwise.</returns>
        /// <exception cref="ArgumentException">Value cannot be null or whitespace.</exception>
        protected async Task<bool> IsTileInstalledAsync([NotNull] string tileGuid)
        {
            if (string.IsNullOrWhiteSpace(tileGuid))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(tileGuid));

            return await this.IsTileInstalledAsync(new Guid(tileGuid));
        }

        /// <summary>
        /// Creates the custom band tile.
        /// </summary>
        /// <param name="tileDefinition">The tile definition.</param>
        protected virtual async Task CreateCustomBandTileAsync(ICustomBandTile tileDefinition)
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
                    await this.bandConnectionsManager.BandClient.TileManager.RemoveTileAsync(tileDefinition.Guid);

                // Add tile 
                await this.bandConnectionsManager.BandClient.TileManager.AddTileAsync(tile);

                // Populate with default data
                await this.bandConnectionsManager.BandClient.TileManager.SetPagesAsync(
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

        /// <summary>
        /// Loads the icon.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Returns the <see cref="BandIcon"/> for image on specified path.</returns>
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
