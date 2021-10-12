using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data.Entities
{
    public abstract class BaseBoxSideValueEntity
    {
        [Key]
        public long BoxSideId { get; set; }

        public virtual BoxSide BoxSide { get; set; }
    }
}
