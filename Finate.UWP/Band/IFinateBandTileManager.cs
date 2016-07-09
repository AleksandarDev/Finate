using System.Threading.Tasks;

namespace Finate.UWP.Band
{
    /// <summary>
    /// The Finate band tile manager.
    /// </summary>
    public interface IFinateBandTileManager
    {
        /// <summary>
        /// Determines whether budget tile is installed.
        /// </summary>
        Task<bool> IsBudgetTileInstalledAsync();

        /// <summary>
        /// Determines whether transactions tile is installed.
        /// </summary>
        Task<bool> IsTransactionsTileInstalledAsync();

        /// <summary>
        /// Creates the budget tile.
        /// </summary>
        Task CreateBudgetTileAsync();

        /// <summary>
        /// Creates the transactions tile.
        /// </summary>
        Task CreateTransactionsTileAsync();
    }
}