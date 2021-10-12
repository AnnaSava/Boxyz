using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data
{
    public abstract class BaseCultureModel
    {
        public long ContentId { get; set; }

        public string Culture { get; set; }
    }
}
