using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxSideFlatModel : BaseModel
    {
        public long ShapeSideId { get; set; }

        public ShapeSideFlatModel ShapeSide { get; set; }

        public string ConstName { get; set; }

        public string DataType { get; set; }

        public string Title { get; set; }

        public string UniversalValue { get; set; }

        public string Value { get; set; }

        public string Culture { get; set; }
    }
}
