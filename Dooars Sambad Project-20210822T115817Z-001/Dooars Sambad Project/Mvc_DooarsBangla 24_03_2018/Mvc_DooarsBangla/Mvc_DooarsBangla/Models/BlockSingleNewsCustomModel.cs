using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class BlockSingleNewsCustomModel
    {
        public AllNewsTableModel fullNews { get; set; }
        public List<AllNewsTableModel> PopularList { get; set; }
        public PrevNextModel prevnxtList { get; set; }
        
    }
}