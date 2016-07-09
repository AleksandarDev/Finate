using System;
using System.Collections.Generic;
using Finate.UWP.Annotations;
using Finate.UWP.Band;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Serilog;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The band page view model.
    /// </summary>
    /// <seealso cref="Prism.Windows.Mvvm.ViewModelBase" />
    public class BandPageViewModel : ViewModelBase
    {
        private readonly ILogger logger;
        private readonly IBandConnectionsManager bandConnectionsManager;
        private readonly IFinateBandTileManager bandTileManager;
        private string bandName;
        private int defaultAmount;


        /// <summary>
        /// Initializes a new instance of the <see cref="BandPageViewModel"/> class.
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
        public BandPageViewModel(
            [NotNull] ILogger logger,
            [NotNull] IBandConnectionsManager bandConnectionsManager,
            [NotNull] IFinateBandTileManager bandTileManager)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (bandConnectionsManager == null) throw new ArgumentNullException(nameof(bandConnectionsManager));
            if (bandTileManager == null) throw new ArgumentNullException(nameof(bandTileManager));

            this.logger = logger;
            this.bandConnectionsManager = bandConnectionsManager;
            this.bandTileManager = bandTileManager;
        }


        /// <summary>
        /// Called when navigation is performed to a page. You can use this method to load state if it is available.
        /// </summary>
        /// <param name="e">The <see cref="T:Prism.Windows.Navigation.NavigatedToEventArgs" /> instance containing the event data.</param>
        /// <param name="viewModelState">The state of the view model.</param>
        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            // Connect to band
            await this.bandConnectionsManager.ConnectBandAsync();
            this.BandName = this.bandConnectionsManager.BandInfo.Name;

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
