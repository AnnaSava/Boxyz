using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data.Entities
{
    public class Shape : BaseEntity
    {
        public string ConstName { get; set; }

        public long BoardId { get; set; }

        public virtual ShapeBoard Board { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual ICollection<ShapeVersion> Versions { get; set; }
    }
}
