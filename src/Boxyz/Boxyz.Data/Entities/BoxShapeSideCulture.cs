﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxShapeSideCulture : BaseCultureEntity
    {
        public string Title { get; set; }

        [Key]
        public long ShapeSideId { get; set; }

        public virtual BoxShapeSide ShapeSide { get; set; }
    }
}
