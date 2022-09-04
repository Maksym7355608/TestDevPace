using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDevPace.Business.Models.Enums;

namespace TestDevPace.Business.Models
{
    public class SearchAndSort
    {
        public SearchingCategories? Category { get; set; }
        public TypeOfSorting? SortType { get; set; }
    }
}
