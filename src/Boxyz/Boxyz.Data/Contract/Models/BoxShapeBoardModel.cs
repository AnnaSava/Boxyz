using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public class BoxShapeBoardModel : BaseModel
    {
        public string Name { get; set; }

        public virtual List<BoxShapeBoardCultureModel> Cultures { get; set; }

        public virtual List<BoxShapeBoardModel> ChildBoards { get; set; }

        public int Level { get; set; }

        public string Path { get; set; }
    }
}
