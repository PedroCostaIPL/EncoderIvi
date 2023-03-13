using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderIvi.Message
{
    public class Header
    {
        public int protocolVersion { get; set; }
        public int messageID { get; set; }
        public int stationID { get; set; }
    }
}
