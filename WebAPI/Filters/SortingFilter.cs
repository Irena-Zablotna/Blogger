using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Helpers.WebAPI.Helpers;

namespace WebAPI.Filters
{
    public class SortingFilter
    {
        public string SortField { get; set; }
        public bool Ascending { get; set; } = true;

        public SortingFilter()
        {
            SortField = "Id";
        }

        public SortingFilter(string choice, bool ascending)
        {
            var sortFields = SortingHelper.GetSortFields();

            choice = choice.ToLowerInvariant();

            if (sortFields.Select(x => x.Key).Contains(choice))

            { 
                choice = sortFields.Where(x => x.Key == choice).Select(x => x.Value).SingleOrDefault(); 
            }

            else
            {
                choice = "Id";
            }
                
            SortField = choice;
            Ascending = ascending;
        }
    
    }
    
}
