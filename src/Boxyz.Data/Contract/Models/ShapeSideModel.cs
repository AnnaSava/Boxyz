using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class ShapeSideModel : BaseModel
    {
        public string ConstName { get; set; }

        public string DataType { get; set; }

        public virtual List<ShapeSideCultureModel> Cultures { get; set; }
    }
}
