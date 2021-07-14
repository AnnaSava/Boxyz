using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class ShapeFlatModel : BaseModel
    {
        public long VersionId { get; set; }

        public string ConstName { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsApproved { get; set; }

        public DateTime Created { get; set; }

        public string Title { get; set; }

        public string Culture { get; set; }

        public List<ShapeSideFlatModel> Sides { get; set; }
    }
}
