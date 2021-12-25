using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdu
{
    public static class Column
    {
        public static double colreinf(double column_depth, double column_width, double reinf_percentage)
        {
            //  depth and width of columns are in inches units

            return column_depth * column_width * reinf_percentage / 100.0;
        }
        

        public static double colreinf(double column_diameter, double reinf_percentage)
        {
            //  depth and width of columns are in inches units

            return (0.785 * column_diameter * column_diameter) * reinf_percentage / 100.0;
        }
    }
}
