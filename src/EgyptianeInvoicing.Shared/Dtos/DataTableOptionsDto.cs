using System.Web;

namespace EgyptianeInvoicing.Shared.Dtos
{
    public class DataTableOptionsDto
    {
        // Pagination
        public int Draw { get; set; } = 1;
        public int Start { get; set; } = 0;
        public int Length { get; set; } = 10;

        // Filter
        public DateTime? FromDate { get; set; } = DateTime.Now;
        public DateTime? ToDate { get; set; } = DateTime.Now;
        public string SearchText { get; set; } = "";

        // Ordering
        public string OrderBy { get; set; }
        public DataTableOptionsDto()
        {
            Draw = 1;
            Start = 0;
            Length = 10;
        }
        public string ToQueryString()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["draw"] = Draw.ToString();
            queryString["start"] = Start.ToString();
            queryString["length"] = Length.ToString();
            queryString["fromDate"] = FromDate?.ToString("yyyy-MM-dd");
            queryString["toDate"] = ToDate?.ToString("yyyy-MM-dd");
            queryString["searchText"] = SearchText;
            queryString["orderBy"] = OrderBy;
            return queryString.ToString();
        }
    }
}
