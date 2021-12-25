//  RECTANGULAR SECTION TORSION DESIGNER (RSTD)
//  ===========================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdu
{
    public class RSTD
    {
        //private static double[] cube_strengths = {1300, 2200, 2900, 3600, 4000, 4200, 4300, 5200, 5300, 6100, 6400,7000, 7600};
        
        #region INPUTS

        public double clear_cover=1.5, beam_width=6, beam_depth=12, fc=4, phi=0.75, Vu=0, Tu=0;
        public double stirrup_dia=0.375;
        public bool COMPATIBILITY_TORSION;
        public StringBuilder summary, details;

        #endregion // INPUTS

        #region OUTPUTS

        public double Aoh, Pcp, Acp, Ao, Ph, Tcr, design_Tu, At_over_s=0, s_design=0, s_max=0, s_required=0, Al=0, Vc=0;
        public bool TU_IS_LESS_THAN_PHI_TCR_DIV_BY_4 = false;
        public bool TORSION_IGNORED = false;
        public bool SECTION_IS_INADEQUATE = false;

        #endregion // OUTPUTS

        public void Design()
        {
            //  INITIATE REPORTING VARIABLES
            summary = new StringBuilder();
            details = new StringBuilder();

            //  CALCULATE SECTION PROPERTIES
            double cc = clear_cover;
            double b = beam_width; double h = beam_depth;
            double d = h-(cc+stirrup_dia+0.5);
            Aoh = (b - 2 * cc) * (h - 2 * cc);
            Pcp = 2 * (b + h);
            Acp = b * h;
            Ao = 0.85 * Aoh;
            Ph = 2 * ((b - 2 * cc) + (h - 2 * cc));
            Vc = 2 * Math.Sqrt(fc*1000) * b*d;
            
            //  CRITICAL CRACKING TORSION
            Tcr = 4 * Math.Sqrt(fc * 1000) * (Acp * Acp / Pcp);
            double phi_Tcr = phi * Tcr;

            //  CHECK IF THE TORSION CAN BE REDUCED
            if (COMPATIBILITY_TORSION)
            {
                design_Tu = Math.Min(Tu, phi_Tcr);
            }
            else
            {
                design_Tu = Tu;
            }

            //  MAXIMUM SPACING
            s_max = Math.Min(Ph/8.0, 12.0);

            //  CHECK IF THE TORSION CAN BE IGNORED
            if (design_Tu*12.0 <= phi_Tcr / 4.0)
            {
                TU_IS_LESS_THAN_PHI_TCR_DIV_BY_4 = true;
                TORSION_IGNORED = true;
                return;
            }

            //  CHECK FOR SECTION CAPACITY
            double param1 = Vu / (b * d);
            double param2 = design_Tu*12.0 * Ph / (1.7 * Aoh * Aoh);
            double param3 = Vc / (b * d);
            double param4 = 8 * Math.Sqrt(fc * 1000);
            double total_shear = Math.Sqrt(param1 *param1 + param2 * param2);
            double total_capacity = phi * (param3 + param4);
            if (total_capacity < total_shear)
            {
                SECTION_IS_INADEQUATE = true;
                return;
            }

            //  CALCULATE TRANSVERSE REINFORCEMENT
            double Tn_kip_inch = design_Tu * 12.0 / phi;
            At_over_s = Tn_kip_inch / (120 * Ao);
            double At = 0.785 * stirrup_dia * stirrup_dia; //   area of one leg of stirrup
            s_design = At / At_over_s;
            s_required = Math.Min(s_design, s_max);

            //  CALCULATE LONGITUDINAL REINFORCEMENT
            Al = At_over_s * Ph;
        }




    }
}
