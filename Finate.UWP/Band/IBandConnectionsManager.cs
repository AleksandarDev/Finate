using System.Threading.Tasks;
using Microsoft.Band;

namespace Finate.UWP.Band
{
    /// <summary>
    /// The band connections manager.
    /// </summary>
    public interface IBandConnectionsManager
    {
        /// <summary>
        /// Gets the band information.
        /// </summary>
        /// <value>
        /// The band information.
        /// </value>
        IBandInfo BandInfo { get; }

        /// <summary>
        /// Gets the band client.
        /// </summary>
        /// <value>
        /// The band client.
        /// </value>
        IBandClient BandClient { get; }

        /// <summary>
        /// Connects the band.
        /// </summary>
        Task ConnectBandAsync();
    }
}