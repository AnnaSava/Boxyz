using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data.Entities
{
    public class BoxSideCulture : BaseCultureEntity<BoxSide>
    {
        public string Value { get; set; }
    }
}
