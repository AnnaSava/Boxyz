using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxVersion : BaseEntity, IVersion
    {
        public long ContentId { get; set; }

        public virtual Box Content { get; set; }

        public long ShapeVersionId { get; set; }

        public virtual ShapeVersion ShapeVersion { get; set; }

        public DateTime Created { get; set; }

        public bool IsApproved { get; set; }

        public virtual ICollection<BoxSide> Sides { get; set; }
    }
}
