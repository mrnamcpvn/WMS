using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Dtos
{
    public class WMSF_Carton_LocatDto
    {
        public string Factory_ID { get; set; }

        public string Barcode { get; set; }

        public string Warehouse_ID { get; set; }

        public string Location_ID { get; set; }

        public string Order_ID { get; set; }

        public decimal Carton_Pairs { get; set; }

        public decimal Volume { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Time { get; set; }

        public string Status_Type { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Time { get; set; }
    }
}
