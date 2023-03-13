using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderIvi.Message
{
    public class Line
    {
        public DeltaPositions? deltaPositions { get; set; }
        public AbsolutePositions? absolutePositions { get; set; }

    }
}
