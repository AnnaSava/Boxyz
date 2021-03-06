using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data
{
    public class ShapeVersionModel : BaseModel
    {
        public long ContentId { get; set; }

        public bool IsApproved { get; set; }

        public DateTime Created { get; set; }

        public virtual List<ShapeSideModel> Sides { get; set; }

        public virtual List<ShapeVersionCultureModel> Cultures { get; set; }
    }
}
