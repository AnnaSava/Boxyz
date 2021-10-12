using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data.Entities
{
    public class BoxSide : BaseEntity
    {
        public long BoxVersionId { get; set; }

        public virtual BoxVersion BoxVersion { get; set; }

        public long ShapeSideId { get; set; }

        public virtual ShapeSide ShapeSide { get; set; }

        public string UniversalValue { get; set; }

        public virtual ICollection<BoxSideCulture> Cultures { get; set; }

        public virtual BoxSideFloat Float { get; set; }

        public virtual BoxSideInteger Integer { get; set; }

        public virtual BoxSideMoney Money { get; set; }

        public virtual BoxSidePoint Point { get; set; }

        public virtual ICollection<BoxSideLink> Links { get; set; }
    }
}
