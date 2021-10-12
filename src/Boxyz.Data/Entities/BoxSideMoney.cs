using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data.Entities
{
   public class BoxSideMoney : BaseBoxSideValueEntity
    {
        public decimal Value { get; set; }

        public string Currency { get; set; }
    }
}
