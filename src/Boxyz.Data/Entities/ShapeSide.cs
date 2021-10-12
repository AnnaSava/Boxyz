using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data.Entities
{
    public class ShapeSide : BaseEntity
    {
        public string ConstName { get; set; }

        public long ShapeVersionId { get; set; }

        public virtual ShapeVersion ShapeVersion { get; set; }

        public string DataType { get; set; }

        public int OrderNumber { get; set; }

        public virtual ICollection<ShapeSideCulture> Cultures { get; set; }
    }
}
