using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mathematics
{
    public static class Mathematics
    {
        /// <summary>
        /// Converts a set of carhtesian coordinates into their isometric counterparts and
        /// returns them as a tuple.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns> A tuple with the converted coordinates.</returns>
        private static (int isoX, int isoY) GetIsoCoordinates(int x, int y)
        {
            var isoX = (x - y);
            var isoY = (x + y) / 2;

            return (isoX, isoY);
        }

        public static double Add(double num1, double num2)
        {
            return num1 + num2;
        }
    }
}
