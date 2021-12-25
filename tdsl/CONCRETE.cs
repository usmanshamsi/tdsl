using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdu
{
    public static class Concrete
    {
        private static double[] cube_strengths = {1300, 2200, 2900, 3600, 4000, 4200, 4300, 5200, 5300, 6100, 6400,7000, 7600};
        private static double[] cube_to_cylinder_factors = {.77, .77, .76, .81, .87, .91, .91, .89, .94, .87, .92, .91, .96};
        private static double[] cyliner_strengths = { 1000, 1700, 2200, 2900, 3500, 3800, 3900, 4600, 5000, 5300, 5900, 6400, 7300 };
        private static int CONVERSION_DATA_LENGTH = cube_strengths.Length;

        public static double root_fc_with_code_limit(double fc)
        {
            return Math.Min(Math.Sqrt(fc), 100.0);
        }

        public static double cube_to_cylinder_factor_01(double cube_strength)
        {
            //  lower limit
            if (cube_strength <= cube_strengths[0])
                return cube_to_cylinder_factors[0];

            //  upper limit
            if (cube_strength >= cube_strengths[CONVERSION_DATA_LENGTH-1])
                return cube_to_cylinder_factors[CONVERSION_DATA_LENGTH-1];

            //  check intermediate values
            int i;
            for (i = 0; i < CONVERSION_DATA_LENGTH-1; i++)
            {
                //  lower limit
                if (cube_strength == cube_strengths[i])
                    return cube_to_cylinder_factors[i];

                //  upper limit
                if (cube_strength == cube_strengths[i+1])
                    return cube_to_cylinder_factors[i+1];

                //  interpolate between upper and lower limit
                if((cube_strength > cube_strengths[i]) && (cube_strength < cube_strengths[i+1]))
                    return cube_to_cylinder_factors[i] + (cube_to_cylinder_factors[i + 1] - cube_to_cylinder_factors[i]) / (cube_strengths[i + 1] - cube_strengths[i]) * (cube_strength- cube_strengths[i]);
            }

            return 0;
        }

        public static double cube_to_cylinder_factor_02(double cylinder_strength)
        {
            //  lower limit
            if (cylinder_strength <= cyliner_strengths[0])
                return cube_to_cylinder_factors[0];

            //  upper limit
            if (cylinder_strength >= cyliner_strengths[CONVERSION_DATA_LENGTH-1])
                return cube_to_cylinder_factors[CONVERSION_DATA_LENGTH-1];

            //  check intermediate values
            int i;
            for (i = 0; i < CONVERSION_DATA_LENGTH-1; i++)
            {
                //  lower limit
                if (cylinder_strength == cyliner_strengths[i])
                    return cube_to_cylinder_factors[i];

                //  upper limit
                if (cylinder_strength == cyliner_strengths[i + 1])
                    return cube_to_cylinder_factors[i + 1];

                //  interpolate between upper and lower limit
                if ((cylinder_strength > cyliner_strengths[i]) && (cylinder_strength < cyliner_strengths[i + 1]))
                    return cube_to_cylinder_factors[i] + (cube_to_cylinder_factors[i + 1] - cube_to_cylinder_factors[i]) / (cyliner_strengths[i + 1] - cyliner_strengths[i]) * (cylinder_strength- cyliner_strengths[i]);
            }

            return 0;
        }
    }
}
