using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderIvi.Message
{
    public class Data
    {
        public Header? header { get; set; }
        public List<Ivi>? ivi { get; set; }
    }
}
