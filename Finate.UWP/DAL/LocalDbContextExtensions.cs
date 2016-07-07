using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Practices.Unity;
using Prism.Unity.Windows;

namespace Finate.UWP.DAL
{
    /// <summary>
    /// The extensions for <see cref="ILocalDbContext"/> class.
    /// </summary>
    public static class LocalDbContextExtensions
    {
        /// <summary>
        /// Saves the context.
        /// </summary>
        /// <returns>Returns <c>True</c> if context was saved successfully; <c>False</c> otherwise.</returns>
        public static async Task<bool> TrySaveContextAsync(this ILocalDbContext context)
        {
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var logger = PrismUnityApplication.Current.Container.Resolve<ILogger>();
                logger.LogError(new EventId(0), ex, "Failed to save context changes.");
                return false;
            }
        }
    }
}