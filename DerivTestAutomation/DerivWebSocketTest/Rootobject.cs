using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerivWebSocketTest
{
    public class Rootobject
    {
        public Echo_Req echo_req { get; set; }
        public string msg_type { get; set; }
        public States_List[] states_list { get; set; }
    }

    public class Echo_Req
    {
        public string states_list { get; set; }
    }

    public class States_List
    {
        public string text { get; set; }
        public string value { get; set; }
    }
}
