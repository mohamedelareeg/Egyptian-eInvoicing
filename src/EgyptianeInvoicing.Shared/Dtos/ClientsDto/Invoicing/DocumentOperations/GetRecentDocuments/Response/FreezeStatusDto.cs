namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response
{
    public class FreezeStatusDto
    {
        public bool Frozen { get; set; }
        public int? Type { get; set; }
        public int? Scope { get; set; }
        public DateTime? ActionDate { get; set; }
        public string AuCode { get; set; }
        public string AuName { get; set; }
    }
}
