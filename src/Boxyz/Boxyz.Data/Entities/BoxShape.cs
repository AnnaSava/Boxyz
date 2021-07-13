using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxShape : BaseEntity
    {
        public string ConstName { get; set; }

        public long BoardId { get; set; }

        public virtual BoxShapeBoard Board { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual ICollection<BoxShapeVersion> Versions { get; set; }
    }
}
