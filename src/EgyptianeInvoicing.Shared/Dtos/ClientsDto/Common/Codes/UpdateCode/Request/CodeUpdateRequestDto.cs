namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.UpdateCode.Request
{
    public class CodeUpdateRequestDto
    {
        public string CodeDescriptionPrimaryLang { get; set; }
        public string CodeDescriptionSecondaryLang { get; set; }
        public DateTime? ActiveTo { get; set; }
        public string LinkedCode { get; set; }
    }


}
