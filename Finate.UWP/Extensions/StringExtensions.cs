using System;
using Windows.UI;

namespace Finate.UWP.Extensions
{
    /// <summary>
    /// The extensions for <see cref="System.String"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the string HEX color value to <see cref="Windows.UI.Color"/>.
        /// </summary>
        /// <param name="hexValue">The hex value.</param>
        /// <returns>Returns the <see cref="Windows.UI.Color"/> that represents given HEX color value.</returns>
        public static Color GetColorFromHexString(this string hexValue)
        {
            var a = Convert.ToByte(hexValue.Substring(1, 2), 16);
            var r = Convert.ToByte(hexValue.Substring(3, 2), 16);
            var g = Convert.ToByte(hexValue.Substring(5, 2), 16);
            var b = Convert.ToByte(hexValue.Substring(7, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }
    }
}
