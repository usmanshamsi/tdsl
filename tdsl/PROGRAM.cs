using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace tdsl
{
    static class Program
    {
        

        //public static string disclaimer = "This program has been carefully designed to produce correct results as " +
        //                                            "per the requirements of ACI-318 Design Code. The program has " +
        //                                            "been tested on design examples from reference books and every effort " +
        //                                            "has been made to make it as correct as possible. However, the " +
        //                                            "user is completely responsible in all respects to judge the " +
        //                                            "accuracy of the results before relying on and taking further " +
        //                                            "decisions based on these results. The programmer, advertiser, " +
        //                                            "distributor or anybody else involved in the production and " +
        //                                            "distribution of the program takes no responsibility, in any " +
        //                                            "form, for any loss or harm to anybody due to any incorrect result " +
        //                                            "or any misinterpretation of the results.";
        


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        

    }
}
