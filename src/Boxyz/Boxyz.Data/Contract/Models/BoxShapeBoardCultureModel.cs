using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxShapeBoardCultureModel : BaseCultureModel
    {
        public long BoardId { get; set; }

        public string Title { get; set; }        
    }
}
