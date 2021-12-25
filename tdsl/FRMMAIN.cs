using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using sdu;
using sdu_custom_types;
using custom_io_functions;
using FormSerialisation;

namespace tdsl
{
    public partial class frmMain : Form
    {

        //  validation flag
        private int invalidCount = 0;

        //  Constructor
        public frmMain()
        {
            InitializeComponent();

            //  populate list box
            list_bar_designation.Items.AddRange(Rebar.bar_numbers_string);
            list_bar_designation.SelectedIndex = 0;
            
            bttnCalculate.Enabled = invalidCount==0? true: false;
        }

        //  Form options manipulatoin
        private void chkFctNotSpecified_CheckedChanged(object sender, EventArgs e)
        {
            txtFct.Enabled = !(chkTensileStrengthUnknown.Checked);
        }

        //  Calculation
        private void Calculate(object sender, EventArgs e)
        {
            //  Check invalid count
            if (invalidCount > 0) return;

            //  calculate development length
            //  build concrete object
            t_Concrete concrete = new t_Concrete();
            concrete.fc = customIO.atof(ref txtfc);
            concrete.fct = customIO.atof(ref txtFct);
            concrete.fct_is_known = !(chkTensileStrengthUnknown.Checked);
            concrete.light_weight = chkAggregateIsLightWeight.Checked;
            
            //  build main reinforcement
            t_Rebar rebar = new t_Rebar();
            rebar.fy = customIO.atof(ref txtFy);
            rebar.diameter = Rebar.bar_diameters[list_bar_designation.SelectedIndex];
            rebar.epoxy_coated = chkRebarIsEpoxyCoated.Checked;

            //  transverse reinf
            double atr = customIO.atof(ref txtAtrWithinSpacingS);
            double fyt = customIO.atof(ref txtFyt);
            double s = customIO.atof(ref txtSpacingS);
            double n = customIO.atof(ref txtNumberOfBarsDeveloped);

            //  cover and spacing
            double clear_cover = customIO.atof(ref txtClearCover);
            double clear_spacing = customIO.atof(ref txtClearSpacing);

            //  other inputs


            //  calculate DL
            double tdl;
            
            if (radAccurate.Checked)
            {
                tdl = Rebar.tension_development_length_1(concrete, rebar, atr, fyt, s, n, clear_cover, clear_spacing, 
                                chkMoreThan12inchConcreteBelowRebar.Checked);

            }
            else if (radIgnoreTR.Checked)
            {
                tdl = Rebar.tension_development_length_2(concrete, rebar, clear_cover, clear_spacing,
                                chkMoreThan12inchConcreteBelowRebar.Checked);
            }
            else if (radIgnoreTRandCS.Checked)
            {
                tdl = Rebar.tension_development_length_3(concrete, rebar, (CScondition1.Checked | CScondition2.Checked),
                                chkMoreThan12inchConcreteBelowRebar.Checked);
            }
            else
                tdl = 0;

            //  display DL

            lblDLinch.Text = (Math.Round(tdl, 1)).ToString() + " inch";
            lblDLmm.Text = (Math.Round(tdl * 25.4, 0)).ToString() + " mm";

        //  calculate splice length
            double tsl;

            if (radClassB.Checked)
                tsl = 1.3 * tdl;
            else
                tsl = tdl;

            //  display SL

            lblSLinch.Text = (Math.Round(tsl, 1)).ToString() + " inch";
            lblSLmm.Text = (Math.Round(tsl * 25.4, 0)).ToString() + " mm";


            }

        //  Data Entry And Validation
        private void txtfc_Validating(object sender, CancelEventArgs e)
        {
            double value;
            var tb = sender as ComboBox;

            if ((!Double.TryParse(tb.Text, out value)) | (value < 0.0))
            {
                if (tb.BackColor == System.Drawing.SystemColors.Window)
                {
                    tb.BackColor = Color.Red;
                    invalidCount++;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }

            }
            else
            {
                if (tb.BackColor == Color.Red)
                {
                    tb.BackColor = System.Drawing.SystemColors.Window;
                    invalidCount--;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }

            }
        }

        private void txtFct_Validating(object sender, CancelEventArgs e)
        {
            double value;
            var tb = sender as ComboBox;

            if ((!Double.TryParse(tb.Text, out value)) | (value < 0.0))
            {
                if (tb.BackColor == System.Drawing.SystemColors.Window)
                {
                    tb.BackColor = Color.Red;
                    invalidCount++;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }

            }
            else
            {
                if (tb.BackColor == Color.Red)
                {
                    tb.BackColor = System.Drawing.SystemColors.Window;
                    invalidCount--;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }
            }
        }

        private void txtFy_Validating(object sender, CancelEventArgs e)
        {
            double value;
            var tb = sender as ComboBox;

            if ((!Double.TryParse(tb.Text, out value)) | (value < 0.0))
            {
                if (tb.BackColor == System.Drawing.SystemColors.Window)
                {
                    tb.BackColor = Color.Red;
                    invalidCount++;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }

            }
            else
            {
                if (tb.BackColor == Color.Red)
                {
                    tb.BackColor = System.Drawing.SystemColors.Window;
                    invalidCount--;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }
            }
        }

        private void txtNumberOfBarsDeveloped_Validating(object sender, CancelEventArgs e)
        {
            int value;
            var tb = sender as ComboBox;

            if ((!Int32.TryParse(tb.Text, out value)) | (value < 0.0))
            {
                if (tb.BackColor == System.Drawing.SystemColors.Window)
                {
                    tb.BackColor = Color.Red;
                    invalidCount++;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }

            }
            else
            {
                if (tb.BackColor == Color.Red)
                {
                    tb.BackColor = System.Drawing.SystemColors.Window;
                    invalidCount--;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }
            }
        }

        private void txtClearCover_Validating(object sender, CancelEventArgs e)
        {
            double value;
            var tb = sender as ComboBox;

            if ((!Double.TryParse(tb.Text, out value)) | (value < 0.0))
            {
                if (tb.BackColor == System.Drawing.SystemColors.Window)
                {
                    tb.BackColor = Color.Red;
                    invalidCount++;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }

            }
            else
            {
                if (tb.BackColor == Color.Red)
                {
                    tb.BackColor = System.Drawing.SystemColors.Window;
                    invalidCount--;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }
            }
        }

        private void txtClearSpacing_Validating(object sender, CancelEventArgs e)
        {
            double value;
            var tb = sender as ComboBox;

            if ((!Double.TryParse(tb.Text, out value)) | (value < 0.0))
            {
                if (tb.BackColor == System.Drawing.SystemColors.Window)
                {
                    tb.BackColor = Color.Red;
                    invalidCount++;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }

            }
            else
            {
                if (tb.BackColor == Color.Red)
                {
                    tb.BackColor = System.Drawing.SystemColors.Window;
                    invalidCount--;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }
            }
        }

        private void txtAtrWithinSpacingS_Validating(object sender, CancelEventArgs e)
        {
            double value;
            var tb = sender as ComboBox;

            if ((!Double.TryParse(tb.Text, out value)) | (value < 0.0))
            {
                if (tb.BackColor == System.Drawing.SystemColors.Window)
                {
                    tb.BackColor = Color.Red;
                    invalidCount++;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }

            }
            else
            {
                if (tb.BackColor == Color.Red)
                {
                    tb.BackColor = System.Drawing.SystemColors.Window;
                    invalidCount--;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }
            }
        }

        private void txtFyt_Validating(object sender, CancelEventArgs e)
        {
            double value;
            var tb = sender as ComboBox;

            if ((!Double.TryParse(tb.Text, out value)) | (value < 0.0))
            {
                if (tb.BackColor == System.Drawing.SystemColors.Window)
                {
                    tb.BackColor = Color.Red;
                    invalidCount++;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }

            }
            else
            {
                if (tb.BackColor == Color.Red)
                {
                    tb.BackColor = System.Drawing.SystemColors.Window;
                    invalidCount--;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }
            }
        }

        private void txtSpacingS_Validating(object sender, CancelEventArgs e)
        {
            double value;
            var tb = sender as ComboBox;

            if ((!Double.TryParse(tb.Text, out value)) | (value < 0.0))
            {
                if (tb.BackColor == System.Drawing.SystemColors.Window)
                {
                    tb.BackColor = Color.Red;
                    invalidCount++;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }

            }
            else
            {
                if (tb.BackColor == Color.Red)
                {
                    tb.BackColor = System.Drawing.SystemColors.Window;
                    invalidCount--;
                    bttnCalculate.Enabled = invalidCount == 0 ? true : false;
                }
            }
        }

        private void radAccurate_CheckedChanged(object sender, EventArgs e)
        {
            groupTransverseReinf.Enabled = !(radIgnoreTR.Checked | radIgnoreTRandCS.Checked);

            if (radAccurate.Checked)
            {
                groupCoverSpacing1.Visible = true;
                groupCoverSpacing2.Visible = false;

            }
        }

        private void radIgnoreTR_CheckedChanged(object sender, EventArgs e)
        {
            groupTransverseReinf.Enabled = !(radIgnoreTR.Checked | radIgnoreTRandCS.Checked);

            if (radIgnoreTR.Checked)
            {
                groupCoverSpacing1.Visible = true;
                groupCoverSpacing2.Visible = false;
            }
            
        }

        private void radIgnoreTRandCS_CheckedChanged(object sender, EventArgs e)
        {
            groupTransverseReinf.Enabled = !(radIgnoreTR.Checked | radIgnoreTRandCS.Checked);

            if (radIgnoreTRandCS.Checked)
            {
                groupCoverSpacing1.Visible = false;
                groupCoverSpacing2.Visible = true;
            }

        }

        private void list_bar_designation_SelectedIndexChanged(object sender, EventArgs e)
        {
            double barDia = Rebar.bar_diameters[(sender as ListBox).SelectedIndex];

            CScondition1.Text = "Clear Cover >= " + barDia.ToString() + " inch, Clear Spacing >=  " + (2 * barDia).ToString() + " inch";
            CScondition2.Text = "Clear Cover >= " + barDia.ToString() + " inch, Clear Spacing >=  " + barDia.ToString() + " inch and Code Requirement of minimum transfer reinforcement meet";

        }

        private void frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormSerialisor.Serialise(this, Application.StartupPath + @"\tdsl_frmMain_state.xml");
        }

        private void frm_main_Load(object sender, EventArgs e)
        {
            FormSerialisor.Deserialise(this, Application.StartupPath + @"\tdsl_frmMain_state.xml");
        }

        private void list_bar_designation_DoubleClick(object sender, EventArgs e)
        {
            bttnCalculate.PerformClick();
        }
                             
    }
}
