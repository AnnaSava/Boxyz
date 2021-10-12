using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data
{
    public class ShapeModel : BaseModel
    {
        public string ConstName { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual List<ShapeVersionModel> Versions { get; set; }
    }
}
