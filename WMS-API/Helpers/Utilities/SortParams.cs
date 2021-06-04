namespace WMS_API.Helpers.Utilities
{
    public class SortParams
    {
        public string SortColumn { get; set; }
        public string SortBy { get; set; }
        public string SortClass { get; set; }
    }

    public static class SortBy
    {
        public const string Asc = "ASC";
        public const string Desc = "DESC";
    }
}