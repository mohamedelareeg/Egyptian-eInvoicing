namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.CreateEGSCode.Request
{
    public class CreateEGSCodeUsageItemDto
    {
        public string CodeType { get; set; }
        public string ParentCode { get; set; }
        public string ItemCode { get; set; }
        public string CodeName { get; set; }
        public string CodeNameAr { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
        public string Description { get; set; }
        public string DescriptionAr { get; set; }
        public string RequestReason { get; set; }
        public string LinkedCode { get; set; }
    }
}
