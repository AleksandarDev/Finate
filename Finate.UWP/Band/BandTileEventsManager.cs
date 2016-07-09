using System;
using System.Threading.Tasks;
using Finate.UWP.Annotations;
using Microsoft.Band.Tiles;
using Serilog;

namespace Finate.UWP.Band
{
    public class BandTileEventsManager
    {
        private readonly ILogger logger;
        private readonly IBandConnections bandConnections;

        public BandTileEventsManager(
            [NotNull] ILogger logger,
            [NotNull] IBandConnections bandConnections)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (bandConnections == null) throw new ArgumentNullException(nameof(bandConnections));

            this.logger = logger;
            this.bandConnections = bandConnections;
        }


        public async Task StartTrackingAsync()
        {
            try
            {
                this.bandConnections.BandClient.TileManager.TileButtonPressed += TileManagerOnTileButtonPressed;
                this.bandConnections.BandClient.TileManager.TileOpened += TileManagerOnTileOpened;
                this.bandConnections.BandClient.TileManager.TileClosed += TileManagerOnTileClosed;
                await this.bandConnections.BandClient.TileManager.StartReadingsAsync();
            }
            catch (Exception ex)
            {
                this.logger.Warning(ex, "Error while starting reading tile inputs.");
            }
        }

        public async Task StopTrackingAsync()
        {
            try
            {
                await this.bandConnections.BandClient.TileManager.StopReadingsAsync();
            }
            catch (Exception ex)
            {
                this.logger.Warning(ex, "Error while stopping reading tile inputs.");
            }
            finally
            {
                this.bandConnections.BandClient.TileManager.TileButtonPressed -= TileManagerOnTileButtonPressed;
                this.bandConnections.BandClient.TileManager.TileOpened -= TileManagerOnTileOpened;
                this.bandConnections.BandClient.TileManager.TileClosed -= TileManagerOnTileClosed;
            }
        }
        
        private void TileManagerOnTileClosed(object sender, BandTileEventArgs<IBandTileClosedEvent> bandTileEventArgs)
        {
        }

        private void TileManagerOnTileOpened(object sender, BandTileEventArgs<IBandTileOpenedEvent> bandTileEventArgs)
        {
        }

        private void TileManagerOnTileButtonPressed(object sender, BandTileEventArgs<IBandTileButtonPressedEvent> bandTileEventArgs)
        {
        }
    }
}