using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.Dtos
{
    public class ObjectSearchDto
    {
        public string WareHouseId { get; set; }
    
        public string BuildingId { get; set; }
        public string FloorId { get; set; }
        public string AreaId { get; set; }
        public string RackNo { get; set; }
        public string PoNo { get; set; }
        public string DateType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SortBy { get; set; }
        public string Function { get; set; }
    }
}
