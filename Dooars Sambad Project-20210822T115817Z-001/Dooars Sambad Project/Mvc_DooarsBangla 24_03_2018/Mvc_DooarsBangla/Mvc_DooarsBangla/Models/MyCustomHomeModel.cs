using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class MyCustomHomeModel
    {
        public List<MarqueeModel> MarqueeList { get; set; }

        public List<AddScrollerImageModel> ScrollerList { get; set; }

        public List<AllNewsTableModel> LatestList { get; set; }

        public List<AllNewsTableModel> PopularList { get; set; }

        public List<PhotoGalleryModel> PhotoGallery { get; set; }

        public List<AddFeedBackModel> listFeedback { get; set; }

        public IDModel idm { get; set; }

    }
}