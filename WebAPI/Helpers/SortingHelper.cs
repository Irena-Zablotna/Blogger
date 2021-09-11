using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    namespace WebAPI.Helpers
    {
        public class SortingHelper
        {
            public static KeyValuePair<string, string>[] GetSortFields()
            {
                var kvp1= new KeyValuePair<string, string>("title", "Title");
                var kvp2 = new KeyValuePair<string, string>("creationdate", "Created");
                return new[] { kvp1, kvp2 };
            }
        }

        //public class SortFields
        //{
        //    public static KeyValuePair<string, string> Title { get; } = new KeyValuePair<string, string>("title", "Title");
        //    public static KeyValuePair<string, string> CreationDate { get; } = new KeyValuePair<string, string>("creationdate", "Created");
        //}
    }
}
