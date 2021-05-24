using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.Dtos
{
    public class ChartModelDto
    {
        public string factoryName { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string className { get; set; }
        public List<ChartModelDto> childs { get; set; }
    }
}
