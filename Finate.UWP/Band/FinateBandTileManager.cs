using System;
using System.Threading.Tasks;
using Finate.UWP.Annotations;
using Finate.UWP.Band.Tiles;
using Microsoft.Band.Tiles;
using Serilog;

namespace Finate.UWP.Band
{
    /// <summary>
    /// The Finate band tile manager implementation.
    /// </summary>
    /// <seealso cref="Finate.UWP.Band.BandTileManagerBase" />
    /// <seealso cref="Finate.UWP.Band.IFinateBandTileManager" />
    public class FinateBandTileManager : BandTileManagerBase, IFinateBandTileManager
    {
        private readonly ILogger logger;
        private readonly IBandConnectionsManager bandConnectionsManager;
        private readonly IBandTileManager bandTileManager;


        /// <summary>
        /// Initializes a new instance of the <see cref="FinateBandTileManager"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="bandConnectionsManager">The band connections manager.</param>
        /// <param name="bandTileManager">The band tile manager.</param>
        /// <exception cref="ArgumentNullException">
        /// logger
        /// or
        /// bandConnectionsManager
        /// or
        /// bandTileManager
        /// </exception>
        public FinateBandTileManager(
            [NotNull] ILogger logger,
            [NotNull] IBandConnectionsManager bandConnectionsManager,
            [NotNull] IBandTileManager bandTileManager) 
            : base(logger, bandConnectionsManager)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (bandConnectionsManager == null) throw new ArgumentNullException(nameof(bandConnectionsManager));
            if (bandTileManager == null) throw new ArgumentNullException(nameof(bandTileManager));

            this.logger = logger;
            this.bandConnectionsManager = bandConnectionsManager;
            this.bandTileManager = bandTileManager;
        }


        /// <summary>
        /// Determines whether budget tile is installed.
        /// </summary>
        public async Task<bool> IsBudgetTileInstalledAsync()
        {
            return await this.IsTileInstalledAsync(BudgetBandTile.TileGuid);
        }

        /// <summary>
        /// Determines whether transactions tile is installed.
        /// </summary>
        public async Task<bool> IsTransactionsTileInstalledAsync()
        {
            return await this.IsTileInstalledAsync(TransactionsBandTile.TileGuid);
        }

        /// <summary>
        /// Creates the budget tile.
        /// </summary>
        public async Task CreateBudgetTileAsync()
        {
            try
            {
                await this.CreateCustomBandTileAsync(new TransactionsBandTile());
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Failed to create budget tile.");
            }
        }

        /// <summary>
        /// Creates the transactions tile.
        /// </summary>
        public async Task CreateTransactionsTileAsync()
        {
            try
            {
                await this.CreateCustomBandTileAsync(new TransactionsBandTile());
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Failed to create transactions tile.");
            }
        }
    }
}