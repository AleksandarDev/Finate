using System;
using System.Collections.Generic;
using Finate.UWP.Annotations;
using Finate.UWP.Band;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Serilog;

namespace Finate.UWP.ViewModels
{
    public class BandPageViewModel : ViewModelBase
    {
        private readonly ILogger logger;
        private readonly IBandConnections bandConnections;
        private readonly IFinateBandTileManager bandTileManager;
        private string bandName;
        private int defaultAmount;


        public BandPageViewModel(
            [NotNull] ILogger logger,
            [NotNull] IBandConnections bandConnections,
            [NotNull] IFinateBandTileManager bandTileManager)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (bandConnections == null) throw new ArgumentNullException(nameof(bandConnections));
            if (bandTileManager == null) throw new ArgumentNullException(nameof(bandTileManager));

            this.logger = logger;
            this.bandConnections = bandConnections;
            this.bandTileManager = bandTileManager;
        }


        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            // Connect to band
            await this.bandConnections.ConnectBandAsync();
            this.BandName = this.bandConnections.BandInfo.Name;

            // Create tile
            await this.bandTileManager.CreateTransactionsTileAsync();
        }


        /// <summary>
        /// Gets or set the default amount for the transaction.
        /// </summary>
        public int DefaultAmount
        {
            get { return this.defaultAmount; }
            set { this.SetProperty(ref this.defaultAmount, value); }
        }

        /// <summary>
        /// Gets or sets the connected band name.
        /// </summary>
        public string BandName
        {
            get { return this.bandName; }
            set { this.SetProperty(ref this.bandName, value); }
        }
    }
}
