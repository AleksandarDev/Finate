using System;
using System.Linq;
using System.Threading.Tasks;
using Finate.UWP.Annotations;
using Microsoft.Band;
using Serilog;

namespace Finate.UWP.Band
{
    /// <summary>
    /// The Microsoft Band connections manager.
    /// </summary>
    /// <seealso cref="IBandConnectionsManager" />
    public sealed class BandConnectionsManager : IBandConnectionsManager
    {
        private readonly ILogger logger;

        private IBandInfo bandInfo;
        private IBandClient bandClient;


        /// <summary>
        /// Initializes a new instance of the <see cref="BandConnectionsManager"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">logger</exception>
        public BandConnectionsManager([NotNull] ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            this.logger = logger;
        }


        /// <summary>
        /// Connects the band.
        /// </summary>
        public async Task ConnectBandAsync()
        {
            if (this.bandInfo == null)
                await this.FindBandAsync();
        }

        /// <summary>
        /// Finds the band.
        /// </summary>
        private async Task FindBandAsync()
        {
            try
            {
                // Retrieve paired bands
                var pairedBands = await BandClientManager.Instance.GetBandsAsync();
                if (pairedBands.Length < 1)
                {
                    this.logger.Information("No band available to connect to.");
                    return;
                }

                // Get one of the paired bands
                var selectedBand = pairedBands.FirstOrDefault();
                if (selectedBand == null)
                {
                    this.logger.Information("No band available to connect to - null band.");
                    return;
                }

                // Make connection
                this.bandClient = await BandClientManager.Instance.ConnectAsync(selectedBand);
                this.bandInfo = selectedBand;

                this.logger.Information("Connected with band {BandName}", this.bandInfo.Name);
            }
            catch (Exception ex)
            {
                this.logger.Warning(ex, "Failed to pair with band.");

                this.bandClient?.Dispose();
                this.bandClient = null;
            }
        }

        /// <summary>
        /// Gets the band information.
        /// </summary>
        /// <value>
        /// The band information.
        /// </value>
        public IBandInfo BandInfo => this.bandInfo;

        /// <summary>
        /// Gets the band client.
        /// </summary>
        /// <value>
        /// The band client.
        /// </value>
        public IBandClient BandClient => this.bandClient;
    }
}