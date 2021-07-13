using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxVersionModel : BaseModel
    {
        public virtual BoxShapeVersionModel ShapeVersion { get; set; }

        public DateTime Created { get; set; }

        public bool IsApproved { get; set; }

        public virtual List<BoxSideModel> Sides { get; set; }
    }
}
