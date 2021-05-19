using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WMS_API.Dtos
{
    public class WMSF_Rack_AreaDto
    {
        public string Area_ID { get; set; }

        public string Area_Name { get; set; }

        public string Area_Short_Title { get; set; }

        public string Warehouse_A { get; set; }

        public string Hide_Rack { get; set; }

        public string Audit { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Time { get; set; }
    }
}
