using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class ShapeBoardModel : BaseModel
    {
        public string Name { get; set; }

        public virtual List<ShapeBoardCultureModel> Cultures { get; set; }

        public virtual List<ShapeBoardModel> ChildBoards { get; set; }

        public int Level { get; set; }

        public string Path { get; set; }
    }
}
