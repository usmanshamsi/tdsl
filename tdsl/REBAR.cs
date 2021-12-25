using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sdu_custom_types;
using sdu;

namespace sdu
{
    public static class Rebar
    {
        public static int[] bar_numbers = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 14, 18 };
        public static string[] bar_numbers_string = { "#3", "#4", "#5", "#6", "#7", "#8", "#9",
                                                        "#10","#11","#14","#18"};
        public static int[] bar_number_mm = { 10, 13, 16, 19, 22, 25, 29, 32, 36, 43, 57 };
        public static double[] bar_diameters = {0.375, 0.500, 0.625, 0.750, 0.875, 1.000, 1.128, 
                                                 1.270, 1.410, 1.693, 2.257};
        public static double[] bar_diameters_mm = { 9.5, 12.7, 15.9, 19.1, 22.2, 25.4, 28.7, 32.3, 
                                                      35.8, 43.0, 57.3 };
        public static double[] bar_Areas = {0.11, 0.20, 0.31, 0.44, 0.60, 0.79, 1.00, 1.27, 1.56,
                                               2.25, 4.00};
        public static double[] bar_Areas_mm = { 71, 129, 199, 284, 387, 510, 645, 819, 1006, 1452, 2581 };
        public static double[] kg_per_meter = {0.560, 0.994, 1.552, 2.235, 3.042, 3.973, 5.060, 
                                                  6.404, 7.907, 11.38, 20.24};
        public static double[] lb_per_foot = {0.376, 0.668, 1.043, 1.502, 2.044, 2.670, 3.400,
                                                 4.303, 5.313, 7.65, 13.60};


        /* TENSION DEVELOPMENT LENGTHS
        ------------------------------------------------------------------------------------------------------------------------- */
        public static double tension_development_length_1(t_Concrete conc, t_Rebar bar, double atr, double fyt, double s, double n,
                                          double clear_cover, double clear_spacing, bool more_than_12inch_concrete_below)
        {
            /* variable to hold the return value
            -------------------------------------*/
            double ld;

            /* calculate root fc with code limitations
            -------------------------------------------*/
            double root_fc = Concrete.root_fc_with_code_limit(conc.fc);

            /* calculate code parameters
            -----------------------------*/
            double alpha = more_than_12inch_concrete_below ? 1.3 : 1.0;
            double beta;
            if (bar.epoxy_coated)
            {
                if ((clear_cover < 3.0 * bar.diameter) | (clear_spacing < 6.0 * bar.diameter))
                    beta = 1.5;
                else
                    beta = 1.2;
            }
            else
            {
                beta = 1.0;
            }

            double alpha_beta = Math.Min(alpha*beta, 1.7);
            double gamma = (bar.diameter>0.75)?1.0:0.8;
            double lambda = 1.0;
            if (conc.light_weight)
                lambda = 1.3;
            if (conc.fct_is_known)
                lambda = Math.Max(6.7 * root_fc / conc.fct, 1.0);

            double c = Math.Min(clear_cover + 0.5 * bar.diameter, (clear_spacing + bar.diameter) / 2.0);
            double ktr = (atr * fyt) / (1500 * s * n);
            double c_ktr_over_db= Math.Min((c + ktr) / bar.diameter, 2.5);

            ld = ((3.0 / 40.0) * (bar.fy / root_fc) * (alpha_beta * gamma * lambda) / c_ktr_over_db) * bar.diameter;

            return ld;
        }
        //-------**********************------------
        public static double tension_development_length_2(t_Concrete conc, t_Rebar bar,
                                            double clear_cover, double clear_spacing, bool more_than_12inch_concrete_below)
        {
            /* variable to hold the return value
            -------------------------------------*/
            double ld;

            /* calculate root fc with code limitations
            -------------------------------------------*/
            double root_fc = Concrete.root_fc_with_code_limit(conc.fc);

            /* calculate code parameters
            -----------------------------*/
            double alpha = more_than_12inch_concrete_below ? 1.3 : 1.0;
            double beta;
            if (bar.epoxy_coated)
            {
                if ((clear_cover < 3.0 * bar.diameter) | (clear_spacing < 6.0 * bar.diameter))
                    beta = 1.5;
                else
                    beta = 1.2;
            }
            else
            {
                beta = 1.0;
            }

            double alpha_beta = Math.Min(alpha*beta, 1.7);
            double gamma = (bar.diameter>0.75)?1.0:0.8;
            double lambda = 1.0;
            if (conc.light_weight)
                lambda = 1.3;
            if (conc.fct_is_known)
                lambda = Math.Max(6.7 * root_fc / conc.fct, 1.0);

            double c = Math.Min(clear_cover + 0.5 * bar.diameter, (clear_spacing + bar.diameter) / 2.0);
            /* IGNORE TRANSVERSE REINFORCEMENT AND PUT KTR = 0 */
            double ktr = 0;
            double c_ktr_over_db= Math.Min((c + ktr) / bar.diameter, 2.5);

            ld = ((3.0 / 40.0) * (bar.fy / root_fc) * (alpha_beta * gamma * lambda) / c_ktr_over_db) * bar.diameter;

            return ld;
        }
        //-------------*******************----------------
        public static double tension_development_length_3(t_Concrete conc, t_Rebar bar, bool code_criteria_of_cover_and_spacing_meet,
                                            bool more_than_12inch_concrete_below)
        {
            /* variable to hold the return value
            -------------------------------------*/
            double ld;

            /* calculate root fc with code limitations
            -------------------------------------------*/
            double root_fc = Concrete.root_fc_with_code_limit(conc.fc);

            /* calculate code parameters
            -----------------------------*/
            double alpha = more_than_12inch_concrete_below ? 1.3 : 1.0;
            double beta;
            if (bar.epoxy_coated)
                beta = 1.5;
            else
                beta = 1.0;

            double alpha_beta = Math.Min(alpha*beta, 1.7);
            double gamma = (bar.diameter>0.75)?1.0:0.8;
            double lambda = 1.0;
            if (conc.light_weight)
                lambda = 1.3;
            if (conc.fct_is_known)
                lambda = Math.Max(6.7 * root_fc / conc.fct, 1.0);

            double c = code_criteria_of_cover_and_spacing_meet ? 1.5 * bar.diameter : 1.0 * bar.diameter;
            /* IGNORE TRANSVERSE REINFORCEMENT AND PUT KTR = 0 */
            double ktr = 0;
            double c_ktr_over_db= Math.Min((c + ktr) / bar.diameter, 2.5);

            ld = ((3.0 / 40.0) * (bar.fy / root_fc) * (alpha_beta * gamma * lambda) / c_ktr_over_db) * bar.diameter;

            return ld;
        }

        /* COMPRESSION DEVELOPMENT LENGTH
-       ----------------------------------------------------------------------------------- */
        public static double compression_development_length(t_Concrete conc, t_Rebar bar)
        {
            /* Variable to hold return value
            ----------------------------------*/
            double ldc;

            /* calculate root fc with code limitations
            -------------------------------------------*/
            double root_fc = Concrete.root_fc_with_code_limit(conc.fc);


            ldc = Math.Max(
                      Math.Max((0.02 * bar.fy / root_fc) * bar.diameter, 0.0003 * bar.fy * bar.diameter),
                      8.0
                      );

            return ldc;
        }

        /* TENSION SPLICE LENGTHS
        ----------------------------------------------------------------------------------------------------------------------- */
        public static double tension_splice_length_1(t_Concrete conc, t_Rebar bar, double atr, double fyt, double s, double n,
                                          double clear_cover, double clear_spacing, bool more_than_12inch_concrete_below)
        {
            return 1.3 * tension_development_length_1(conc, bar, atr, fyt, s, n,
                                                      clear_cover, clear_spacing, more_than_12inch_concrete_below);
        }

        public static double tension_splice_length_2(t_Concrete conc, t_Rebar bar,
                                            double clear_cover, double clear_spacing, bool more_than_12inch_concrete_below)
        {
            return 1.3 * tension_development_length_2(conc, bar,
                                                      clear_cover, clear_spacing, more_than_12inch_concrete_below);
        }

        public static double tension_splice_length_3(t_Concrete conc, t_Rebar bar, bool code_criteria_of_cover_and_spacing_meet,
                                            bool more_than_12inch_concrete_below)
        {
            return 1.3 * tension_development_length_3(conc, bar, code_criteria_of_cover_and_spacing_meet,
                                                      more_than_12inch_concrete_below);
        }

        /* COMPRESSION SPLICE LENGTH
        -------------------------------------------------------------- */
        public static double compression_splice_length(t_Concrete conc, t_Rebar bar)
        {
            /* Variable to hold return value
            ----------------------------------*/
            double lsc;

            if (bar.fy <= 60000)
                lsc = Math.Max(0.0005 * bar.diameter * bar.fy, 12.0);
            else
                lsc = Math.Max((0.0009 * bar.fy - 24) * bar.diameter, 12.0);

            if (conc.fc < 3000.0)
                lsc *= (1.0 + 1.0 / 3.0);

            return lsc;
        }

    }
}
