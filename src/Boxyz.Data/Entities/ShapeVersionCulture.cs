using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class ShapeVersionCulture : BaseCultureEntity<ShapeVersion>
    {
        public string Title { get; set; }
    }
}
