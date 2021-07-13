using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxShapeModel : BaseModel
    {
        public string ConstName { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual List<BoxShapeVersionModel> Versions { get; set; }
    }
}
