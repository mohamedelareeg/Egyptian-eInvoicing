namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response
{
    public class FreezeStatusDto
    {
        public bool frozen { get; set; }
        public int? type { get; set; }
        public int? scope { get; set; }
        public DateTime? actionDate { get; set; }
        public string? auCode { get; set; }
        public string? auName { get; set; }
    }
}
