using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class ShapeSideFlatModel : BaseModel
    {
        public string ConstName { get; set; }

        public string DataType { get; set; }

        public string Title { get; set; }

        public string Culture { get; set; }
    }
}