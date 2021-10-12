using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data.Entities
{
    public class Box : BaseEntity
    {
        public long ShapeId { get; set; }

        public virtual Shape Shape { get; set; }

        public virtual ICollection<BoxVersion> Versions { get; set; }
    }
}
