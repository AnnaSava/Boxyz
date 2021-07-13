using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxShapeSide : BaseEntity
    {
        public string ConstName { get; set; }

        public long ShapeVersionId { get; set; }

        public virtual BoxShapeVersion ShapeVersion { get; set; }

        public string DataType { get; set; }

        public virtual ICollection<BoxShapeSideCulture> Cultures { get; set; }
    }
}
