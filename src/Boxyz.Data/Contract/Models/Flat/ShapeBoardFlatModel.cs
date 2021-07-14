﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class ShapeBoardFlatModel : BaseModel
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Culture { get; set; }

        public int Level { get; set; }

        public string Path { get; set; }
    }
}
