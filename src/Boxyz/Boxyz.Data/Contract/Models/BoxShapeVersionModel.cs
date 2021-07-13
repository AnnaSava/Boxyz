using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxShapeVersionModel : BaseModel
    {
        public bool IsApproved { get; set; }

        public DateTime Created { get; set; }

        public virtual List<BoxShapeSideModel> Sides { get; set; }

        public virtual List<BoxShapeVersionCultureModel> Cultures { get; set; }
    }
}
