using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxShapeVersion : BaseEntity
    {
        public long ShapeId { get; set; }

        public virtual BoxShape Shape { get; set; }

        public bool IsApproved { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<BoxShapeSide> Sides { get; set; }

        public virtual ICollection<BoxShapeVersionCulture> Cultures { get; set; }
    }
}
