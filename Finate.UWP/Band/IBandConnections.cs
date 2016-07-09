using System.Threading.Tasks;
using Microsoft.Band;

namespace Finate.UWP.Band
{
    public interface IBandConnections
    {
        IBandInfo BandInfo { get; }

        IBandClient BandClient { get; }

        Task ConnectBandAsync();
    }
}