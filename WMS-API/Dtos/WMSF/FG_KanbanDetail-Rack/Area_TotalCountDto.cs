using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.Dtos
{
    public class Area_TotalCountDto
    {
        public string Value { get; set; }
        public string Label { get; set; }
        public int TotalCount { get; set; }
        public bool Visible { get; set; }
    }
}
