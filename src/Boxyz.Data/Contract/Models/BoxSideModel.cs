﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxSideModel : BaseModel
    {
        public ShapeSideModel ShapeSide { get; set; }

        public string UniversalValue { get; set; }

        public List<BoxSideCultureModel> Cultures { get; set; }
    }
}
