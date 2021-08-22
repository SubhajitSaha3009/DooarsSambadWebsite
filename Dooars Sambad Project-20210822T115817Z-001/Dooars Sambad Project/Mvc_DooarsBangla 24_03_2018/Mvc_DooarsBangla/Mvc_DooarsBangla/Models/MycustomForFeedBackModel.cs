using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class MycustomForFeedBackModel
    {
        public List<MarqueeModel> MarqueeList { get; set; }

        public List<AllNewsTableModel> PopularList { get; set; }

        public AddFeedBackModel FBmodel { get; set; }

    }
}