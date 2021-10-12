using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data
{
    public class BoxModel : BaseModel
    {
        public ShapeModel Shape { get; set; }

        public long ShapeId { get; set; }

        public List<BoxVersionModel> Versions { get; set; }
    }
}
