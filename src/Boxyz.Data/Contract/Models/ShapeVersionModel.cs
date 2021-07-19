using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class ShapeVersionModel : BaseModel
    {
        public long ShapeId { get; set; }

        public bool IsApproved { get; set; }

        public DateTime Created { get; set; }

        public virtual List<ShapeSideModel> Sides { get; set; }

        public virtual List<ShapeVersionCultureModel> Cultures { get; set; }
    }
}
