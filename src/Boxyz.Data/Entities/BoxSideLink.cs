using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxSideLink : BaseEntity
    {
        public long BoxSideId { get; set; }

        public virtual BoxSide BoxSide { get; set; }

        public long LinkedBoxVersionId { get; set; }

        public virtual BoxVersion LinkedBoxVersion { get; set; }

        public int OrderNumber { get; set; }
    }
}
