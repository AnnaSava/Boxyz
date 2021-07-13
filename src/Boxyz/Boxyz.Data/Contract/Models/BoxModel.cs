using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxModel : BaseModel
    {
        public virtual BoxShapeModel Shape { get; set; }

        public virtual List<BoxVersionModel> Versions { get; set; }
    }
}
