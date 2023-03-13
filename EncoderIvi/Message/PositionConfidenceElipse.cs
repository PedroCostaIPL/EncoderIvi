using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderIvi.Message
{
    public class PositionConfidenceElipse
    {
        public int semiMajorConfidence { get; set; }
        public int semiMinorConfidence { get; set; }
        public int semiMajorOrientation { get; set; }
    }
}
