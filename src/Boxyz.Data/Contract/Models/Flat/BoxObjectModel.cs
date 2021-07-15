using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    //public class BoxObjectModel : Dictionary<string, ValueWithType>
    public class BoxObjectModel : Dictionary<string, object>
    {
        public long Id { get; set; }
    }
}
