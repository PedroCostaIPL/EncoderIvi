﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderIvi.Message
{
    public class IviContainer
    {
        public Glc? glc { get; set; }
        public List<Giv>? giv { get; set; }
        public List<Tc>? tc { get; set; }
    }
}
