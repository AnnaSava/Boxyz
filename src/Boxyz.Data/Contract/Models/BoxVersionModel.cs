using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxVersionModel : BaseModel
    {
        public ShapeVersionModel ShapeVersion { get; set; }

        public long ShapeVersionId { get; set; }

        public DateTime Created { get; set; }

        public bool IsApproved { get; set; }

        public List<BoxSideModel> Sides { get; set; }
    }
}
