using System.Collections.Generic;

namespace WMS_API.Helpers.Utilities
{
    public class LocationParamDTO
    {
        public SearchParam SearchParam { get; set; }
        public SortParams[] SortParams { get; set; }

    }
}