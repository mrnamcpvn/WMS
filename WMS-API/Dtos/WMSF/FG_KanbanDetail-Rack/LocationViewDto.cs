using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.Dtos
{
    public class LocationViewDto
    {
        public string Factory_ID { get; set; }
        public string Location_ID { get; set; }
        public string Barcode { get; set; }
        public string Order_ID { get; set; }
        public decimal Carton_Pairs { get; set; }
        public string Location_Name { get; set; }
        public string Warehouse_Name { get; set; }
        public string Warehouse_Id { get; set; }
        public string Building_Name { get; set; }
        public string Building_Id { get; set; }
        public string Floor_Name { get; set; }
        public string Floor_Id { get; set; }
        public string Area_Name { get; set; }
        public string Area_ID { get; set; }
        public string Article { get; set; }
        public string Model_ID { get; set; }
        public DateTime? Comfirmed_Date { get; set; }
        public decimal? Order_Qty { get; set; }
        public decimal? Stock_Qty { get; set; }
        public decimal? CBM { get; set; }
        public decimal? Volume { get; set; }

    }
}
