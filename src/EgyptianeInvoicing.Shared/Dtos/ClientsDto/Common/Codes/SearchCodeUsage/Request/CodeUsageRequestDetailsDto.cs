namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.SearchCodeUsage.Request
{
    public class CodeUsageRequestDetailsDto
    {
        public int CodeUsageRequestID { get; set; }
        public string CodeTypeName { get; set; }
        public int CodeID { get; set; }
        public string ItemCode { get; set; }
        public string CodeName { get; set; }
        public string Description { get; set; }
        public int ParentCodeID { get; set; }
        public string ParentItemCode { get; set; }
        public string ParentLevelName { get; set; }
        public string LevelName { get; set; }
        public DateTime RequestCreationDateTimeUtc { get; set; }
        public DateTime CodeCreationDateTimeUtc { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public OwnerTaxpayerDto OwnerTaxpayer { get; set; }
        public RequestorTaxpayerDto RequestorTaxpayer { get; set; }
        public CodeCategorizationDto CodeCategorization { get; set; }
    }
}
