using System;
using System.Collections.Generic;

namespace Finate.UWP.Band
{
    /// <summary>
    /// The custom band tile.
    /// </summary>
    public interface ICustomBandTile
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the layouts.
        /// </summary>
        /// <value>
        /// The layouts.
        /// </value>
        Dictionary<Guid, dynamic> Layouts { get; }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        Guid Guid { get; }

        /// <summary>
        /// Gets the large icon path.
        /// </summary>
        /// <value>
        /// The large icon path.
        /// </value>
        string LargeIconPath { get; }

        /// <summary>
        /// Gets the small icon path.
        /// </summary>
        /// <value>
        /// The small icon path.
        /// </value>
        string SmallIconPath { get; }
    }
}