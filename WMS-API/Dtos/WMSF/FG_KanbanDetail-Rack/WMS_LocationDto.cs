using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WMS_API.Dtos
{
    public class WMS_LocationDto
    {
        public string Factory_ID { get; set; }

        public string Location_ID { get; set; }

        public string Location_Name { get; set; }

        public string Warehouse_ID { get; set; }

        public string Warehouse_Name { get; set; }

        public string Building_ID { get; set; }

        public string Building_Name { get; set; }

        public string Floor_ID { get; set; }

        public string Floor_Name { get; set; }

        public string Area_ID { get; set; }

        public string Area_Name { get; set; }

        public decimal CBM { get; set; }

        public decimal Max_Percent { get; set; }

        public string Remark { get; set; }

        public string Create_By { get; set; }

        public DateTime Create_Time { get; set; }

        public string Status_Type { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Time { get; set; }
    }
}
