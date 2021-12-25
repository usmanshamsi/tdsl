using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdu
{
    public static class Steel
    {
        //  CONSTANTS
        public const double PHI_WELD_CAPACITY = 0.75;


        public static double fillet_weld_capacity(double weld_length, double weld_thickness, double force_angle, int electrode_number)
        {
            double w = weld_thickness;
            double L = weld_length;

            double fw;
            double theta = Math.PI / 180.0 * force_angle;
            double sine = Math.Sin(theta);
            double sine15 = Math.Pow(sine, 1.5);
            fw = 0.60 * electrode_number * (1.0 + 0.5 * sine15);

            double Rn = 0.707 * w * L * fw;
            double phi_Rn = PHI_WELD_CAPACITY * Rn;

            return phi_Rn;

        }
    }
}
