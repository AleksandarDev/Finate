using System;
using System.Threading.Tasks;
using Finate.UWP.Annotations;
using Finate.UWP.Band.Tiles;
using Microsoft.Band.Tiles;
using Serilog;

namespace Finate.UWP.Band
{
    public class FinateBandTileManager : BandTileManager, IFinateBandTileManager
    {
        private readonly ILogger logger;
        private readonly IBandConnections bandConnections;
        private readonly IBandTileManager bandTileManager;


        public FinateBandTileManager(
            [NotNull] ILogger logger,
            [NotNull] IBandConnections bandConnections,
            [NotNull] IBandTileManager bandTileManager) 
            : base(logger, bandConnections)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (bandConnections == null) throw new ArgumentNullException(nameof(bandConnections));
            if (bandTileManager == null) throw new ArgumentNullException(nameof(bandTileManager));

            this.logger = logger;
            this.bandConnections = bandConnections;
            this.bandTileManager = bandTileManager;
        }


        public async Task<bool> IsBudgetTileInstalledAsync()
        {
            return await this.IsTileInstalledAsync(BudgetBandTile.TileGuid);
        }

        public async Task<bool> IsTransactionsTileInstalledAsync()
        {
            return await this.IsTileInstalledAsync(TransactionsBandTile.TileGuid);
        }

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