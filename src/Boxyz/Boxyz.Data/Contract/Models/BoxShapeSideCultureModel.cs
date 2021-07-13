﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxShapeSideCultureModel : BaseCultureModel
    {
        public string Title { get; set; }

        public long ShapeSideId { get; set; }

        public virtual BoxShapeSideModel ShapeSide { get; set; }
    }
}
