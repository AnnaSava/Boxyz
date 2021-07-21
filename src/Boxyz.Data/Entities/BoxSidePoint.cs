using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxSidePoint : BaseBoxSideValueEntity
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }
    }
}
