using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxFlatModel : BaseModel
    {
        public long ShapeId { get; set; }

        public long VersionId { get; set; }

        public DateTime Created { get; set; }

        public bool IsApproved { get; set; }

        public string Culture { get; set; }

        public long ShapeVersionId { get; set; }
    }
}
