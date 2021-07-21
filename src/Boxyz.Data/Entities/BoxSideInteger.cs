using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxSideInteger : BaseBoxSideValueEntity
    {
        public long Value { get; set; }

        public string Measure { get; set; }
    }
}
