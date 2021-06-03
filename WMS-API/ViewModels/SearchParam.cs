using System;

namespace WMS_API.ViewModels
{
    public class SearchParam
    {
        public string wareHouseId { get; set; }
        public string buildingId { get; set; }
        public string floorId { get; set; }
        public string areaId { get; set; }
        public string rackNo { get; set; }
        public string poNo { get; set; }
        public string dateType { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string sortBy { get; set; }
        public string sortType { get; set; }
        public string function { get; set; }
    }
}