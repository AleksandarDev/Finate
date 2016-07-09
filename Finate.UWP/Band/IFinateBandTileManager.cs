using System.Threading.Tasks;

namespace Finate.UWP.Band
{
    public interface IFinateBandTileManager
    {
        Task<bool> IsBudgetTileInstalledAsync();

        Task<bool> IsTransactionsTileInstalledAsync();

        Task CreateBudgetTileAsync();

        Task CreateTransactionsTileAsync();
    }
}