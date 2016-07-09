using System;
using System.Threading.Tasks;
using Finate.UWP.Annotations;
using Microsoft.Band.Tiles;
using Serilog;

namespace Finate.UWP.Band
{
    /// <summary>
    /// The band tile events manager base.
    /// </summary>
    public abstract class BandTileEventsManagerBase
    {
        private readonly ILogger logger;
        private readonly IBandConnectionsManager bandConnectionsManager;


        /// <summary>
        /// Initializes a new instance of the <see cref="BandTileEventsManagerBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="bandConnectionsManager">The band connections manager.</param>
        /// <exception cref="ArgumentNullException">
        /// logger
        /// or
        /// bandConnectionsManager
        /// </exception>
        protected BandTileEventsManagerBase(
            [NotNull] ILogger logger,
            [NotNull] IBandConnectionsManager bandConnectionsManager)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (bandConnectionsManager == null) throw new ArgumentNullException(nameof(bandConnectionsManager));

            this.logger = logger;
            this.bandConnectionsManager = bandConnectionsManager;
        }


        /// <summary>
        /// Starts the tracking.
        /// </summary>
        public virtual async Task StartTrackingAsync()
        {
            try
            {
                this.bandConnectionsManager.BandClient.TileManager.TileButtonPressed += TileManagerOnTileButtonPressed;
                this.bandConnectionsManager.BandClient.TileManager.TileOpened += TileManagerOnTileOpened;
                this.bandConnectionsManager.BandClient.TileManager.TileClosed += TileManagerOnTileClosed;
                await this.bandConnectionsManager.BandClient.TileManager.StartReadingsAsync();
            }
            catch (Exception ex)
            {
                this.logger.Warning(ex, "Error while starting reading tile inputs.");
            }
        }

        /// <summary>
        /// Stops the tracking.
        /// </summary>
        public virtual async Task StopTrackingAsync()
        {
            try
            {
                await this.bandConnectionsManager.BandClient.TileManager.StopReadingsAsync();
            }
            catch (Exception ex)
            {
                this.logger.Warning(ex, "Error while stopping reading tile inputs.");
            }
            finally
            {
                this.bandConnectionsManager.BandClient.TileManager.TileButtonPressed -= TileManagerOnTileButtonPressed;
                this.bandConnectionsManager.BandClient.TileManager.TileOpened -= TileManagerOnTileOpened;
                this.bandConnectionsManager.BandClient.TileManager.TileClosed -= TileManagerOnTileClosed;
            }
        }

        /// <summary>
        /// Handles the tile closed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="bandTileEventArgs">The <see cref="BandTileEventArgs{IBandTileClosedEvent}"/> instance containing the event data.</param>
        protected virtual void TileManagerOnTileClosed(object sender, BandTileEventArgs<IBandTileClosedEvent> bandTileEventArgs)
        {
        }

        /// <summary>
        /// Handles the tile opened event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="bandTileEventArgs">The <see cref="BandTileEventArgs{IBandTileOpenedEvent}"/> instance containing the event data.</param>
        protected virtual void TileManagerOnTileOpened(object sender, BandTileEventArgs<IBandTileOpenedEvent> bandTileEventArgs)
        {
        }

        /// <summary>
        /// Handles the tile button pressed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="bandTileEventArgs">The <see cref="BandTileEventArgs{IBandTileButtonPressedEvent}"/> instance containing the event data.</param>
        protected virtual void TileManagerOnTileButtonPressed(object sender, BandTileEventArgs<IBandTileButtonPressedEvent> bandTileEventArgs)
        {
        }
    }
}