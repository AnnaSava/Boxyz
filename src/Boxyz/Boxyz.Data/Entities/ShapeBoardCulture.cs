using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class ShapeBoardCulture : BaseCultureEntity
    {
        [Key]
        public long BoardId { get; set; }

        public virtual ShapeBoard Board { get; set; }

        public string Title { get; set; }
    }
}
