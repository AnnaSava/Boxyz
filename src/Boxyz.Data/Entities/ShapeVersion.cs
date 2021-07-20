using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class ShapeVersion : BaseEntity, IVersion
    {
        public long ContentId { get; set; }

        public virtual Shape Content { get; set; }

        public bool IsApproved { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<ShapeSide> Sides { get; set; }

        public virtual ICollection<ShapeVersionCulture> Cultures { get; set; }
    }
}
