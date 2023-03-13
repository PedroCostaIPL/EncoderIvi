using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderIvi.Message
{
    public class ReferencePosition
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public PositionConfidenceElipse? positionConfidenceElipse { get; set; }
        public Altitude? altitude { get; set; }
    }
}
