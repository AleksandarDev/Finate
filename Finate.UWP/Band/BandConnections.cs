using System;
using System.Linq;
using System.Threading.Tasks;
using Finate.UWP.Annotations;
using Microsoft.Band;
using Serilog;

namespace Finate.UWP.Band
{
    public sealed class BandConnections : IBandConnections
    {
        private readonly ILogger logger;

        private IBandInfo bandInfo;
        private IBandClient bandClient;


        public BandConnections([NotNull] ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            this.logger = logger;
        }


        public async Task ConnectBandAsync()
        {
            if (this.bandInfo == null)
                await this.FindBandAsync();
        }

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

        public IBandInfo BandInfo => this.bandInfo;

        public IBandClient BandClient => this.bandClient;
    }
}