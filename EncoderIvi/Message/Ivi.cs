﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderIvi.Message
{
    public class Ivi
    {
        public Mandatory? mandatory { get; set; }
        public List<Optional>? optional { get; set; }
    }
}
