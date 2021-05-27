using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.Dtos
{
    public class ChartModelDto
    {
        public string FactoryName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ClassName { get; set; }
        public bool Visible { get; set; }
        public List<ChartModelDto> Childs { get; set; }
    }
}
