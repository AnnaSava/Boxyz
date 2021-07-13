using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxSide : BaseEntity
    {
        public long BoxVersionId { get; set; }

        public virtual BoxVersion BoxVersion { get; set; }

        public long ShapeSideId { get; set; }

        public virtual BoxShapeSide ShapeSide { get; set; }

        public string UniversalValue { get; set; }

        public virtual ICollection<BoxSideCulture> Cultures { get; set; }
    }
}
