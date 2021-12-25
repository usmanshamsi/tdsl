using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace custom_io_functions
{
    class customIO
    {
        static int tab1=2;
        static int tab2 = 50;
        static int tab3 = 15;

        public static void take_input_integer(ref int variable, ref System.Windows.Forms.TextBox textbox)
        {
            variable = Convert.ToInt32(textbox.Text);
        }

        public static int atoi(ref System.Windows.Forms.TextBox textbox)
        {
            try
            {
                return Convert.ToInt32(textbox.Text);
            }
            catch
            {
                return 0;
            }
        }

        public static double atof(ref System.Windows.Forms.TextBox textbox)
        {
            try
            {
                return Convert.ToDouble(textbox.Text);
            }
            catch
            {
                return 0.0;
            }
        }

        //  FUNCTION FOR REPORT OUTPUTS
        public static void rod3(StringBuilder sb, String lbl, double value, String unit)
        {
            sb.Append(string_times(" ", tab1));
            sb.Append(left_align(lbl, tab2));
            sb.Append(right_align(value.ToString("0.000"), tab3));
            sb.Append(" " + unit + "\r\n");

        }
        public static void rod4(StringBuilder sb, String lbl, double value, String unit)
        {
            sb.Append(string_times(" ", tab1));
            sb.Append(left_align(lbl, tab2));
            sb.Append(right_align(value.ToString("0.0000"), tab3));
            sb.Append(" " + unit + "\r\n");
        }
        public static void roi3(StringBuilder sb, String lbl, int value, String unit)
        {
            sb.Append(string_times(" ", tab1));
            sb.Append(left_align(lbl, tab2));
            sb.Append(right_align((value.ToString() + string_times(" ", 4)), tab3));
            sb.Append(" " + unit + "\r\n");
        }
        public static void ro(StringBuilder sb, String lbl)
        {
            sb.Append(string_times(" ", tab1));
            sb.Append(left_align(lbl, tab2));
            sb.Append("\r\n");
        }
        public static String string_times(string str, int times)
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < times; i++)
                output.Append(str);
            return output.ToString();
        }
        public static String left_align(String text, int width)
        {
            int text_length = text.Length;
            int space_length = width - text_length;
            String spaces = string_times(" ", space_length);
            return text + spaces;
        }
        public static String right_align(String text, int width)
        {
            int text_length = text.Length;
            int space_length = width - text_length;
            String spaces = string_times(" ", space_length);
            return spaces + text;
        }
    }
}
