using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models.ViewModels
{
    //the bundle of information that the tag helper will need to construct the pages
    public class PageNumberingInfo
    {
        public int NumItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumItems { get; set; }

        //calculate pages
        //cast one of the properties as a decimal to force decimal division
        public int NumPages => (int) Math.Ceiling((decimal)TotalNumItems / NumItemsPerPage);
    }
}
