using System;

namespace SalesTaxes_Library
{
    /// <summary>
    /// Some useful methods to deal with decimal numbers
    /// </summary>
    public static class Maths
    {
        /// <summary>
        /// Rounds up a number to the nearest 0.05
        /// </summary>
        public static decimal RoundUpToTheNearest005(this decimal value)
            => Math.Ceiling(value / 0.05M) * 0.05M;
    }
}
