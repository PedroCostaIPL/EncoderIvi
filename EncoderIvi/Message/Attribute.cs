﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace EncoderIvi.Message
{
    public class Attribute
    {
        public DTM DTM { get; set; }
        public EDT EDT { get; set; }
        public int DFLType { get; set; }

        public VED VED { get; set; }
        public DDD DDD { get; set; }
        public SPE SPE { get; set; }

    }
}
