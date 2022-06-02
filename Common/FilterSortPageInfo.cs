using System;

namespace MyMvc4App
{
    public class FilterSortPageInfo
    {
        public string Filters { get; set; }     // array of Filter objects in JSON format
        public string Sort { get; set; }
        public string SortDir { get; set; }
        public int Page { get; set; }

        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public FilterSortPageInfo()
        {
            Filters = "[]";                     // empty array in JSON format
            PageSize = 10;
        }

        // this function uses a delegate to reset it's settings
        public void ResetSettings(Action<FilterSortPageInfo> resetSettings)
        {
            resetSettings(this);
        }
    }

    public class Filter
    {
        public string Field { get; set; }
        public string Value { get; set; }
    }
}
