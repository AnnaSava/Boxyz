using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxShapeVersionCulture : BaseCultureEntity
    {
        public string Title { get; set; }

        [Key]
        public long ShapeVersionId { get; set; }

        public virtual BoxShapeVersion ShapeVersion { get; set; }
    }
}
