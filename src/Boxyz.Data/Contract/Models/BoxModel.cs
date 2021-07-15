﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxModel : BaseModel
    {
        public ShapeModel Shape { get; set; }

        public List<BoxVersionModel> Versions { get; set; }
    }
}