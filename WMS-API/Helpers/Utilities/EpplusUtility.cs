using OfficeOpenXml.Style;

namespace QMS_API.Helpers.Utilities
{
    public static class EpplusUtility
    {
        public static ExcelStyle SetAllBorders(this ExcelStyle style)
        {
            style.Border.Top.Style = ExcelBorderStyle.Thin;
            style.Border.Right.Style = ExcelBorderStyle.Thin;
            style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            style.Border.Left.Style = ExcelBorderStyle.Thin;
            return style;
        }

        public static ExcelStyle SetAlignCenter(this ExcelStyle style)
        {
            style.WrapText = true;
            style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            style.VerticalAlignment = ExcelVerticalAlignment.Center;
            return style;
        }

        public static ExcelStyle SetDateFormat(this ExcelStyle style, bool hasDay = true)
        {
            style.Numberformat.Format = hasDay ? "yyyy/MM/dd" : "yyyy/MM";
            return style;
        }

        public static ExcelStyle SetDateTimeFormat(this ExcelStyle style)
        {
            style.Numberformat.Format = "yyyy/MM/dd hh:mm:ss";
            return style;
        }

        public static ExcelStyle SetPercentFormat(this ExcelStyle style, bool isRound = false)
        {
            style.Numberformat.Format = isRound ? @"#0%" : @"#0.00%";
            return style;
        }
    }
}