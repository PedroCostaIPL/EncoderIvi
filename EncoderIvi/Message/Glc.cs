using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderIvi.Message
{
    public class Glc
    {
        public ReferencePosition? referencePosition { get; set; }
        public object? referencePositionTime { get; set; }
        public Parts? parts { get; set; }
    }
}
