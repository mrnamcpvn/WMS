using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.Dtos
{
    public class RackPairsDto
    {
        public string Area_ID { get; set; }
        
        // Chuyển Area_ID qua int bỏ đi chữ A để OrderBy
        public int Area_Number { get; set; }
        public string Area_Short_Title { get; set; }
        public string Area_Name { get; set; }
        public decimal? Pairs_Subtotal { get; set; }
    }
}
