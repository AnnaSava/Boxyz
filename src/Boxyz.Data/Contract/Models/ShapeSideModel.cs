using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data
{
    public class ShapeSideModel : BaseModel
    {
        public long ShapeVersionId { get; set; }

        public string ConstName { get; set; }

        public string DataType { get; set; }

        public virtual List<ShapeSideCultureModel> Cultures { get; set; }
    }
}
