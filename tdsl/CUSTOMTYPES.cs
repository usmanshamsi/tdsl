using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdu_custom_types
{
    public class t_Concrete
    {
        // Fields
        public double fc;
        public double fct;
        public bool fct_is_known;
        public bool light_weight;
    }

    public class t_Rebar
    {
        // Fields
        public double fy;
        public double diameter;
        public bool epoxy_coated;
    }
}
