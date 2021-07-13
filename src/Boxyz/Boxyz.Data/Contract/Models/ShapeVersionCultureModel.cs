using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class ShapeVersionCultureModel : BaseCultureModel
    {
        public string Title { get; set; }

        public long ShapeVersionId { get; set; }

        public virtual ShapeVersionModel ShapeVersion { get; set; }
    }
}
