﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxSideCulture : BaseCultureEntity
    {
        public long BoxSideId { get; set; }

        public virtual BoxSide BoxSide { get; set; }

        public string Value { get; set; }
    }
}
