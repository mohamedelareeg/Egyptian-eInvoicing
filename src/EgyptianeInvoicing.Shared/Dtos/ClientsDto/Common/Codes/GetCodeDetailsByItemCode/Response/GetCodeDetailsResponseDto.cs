namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.GetCodeDetailsByItemCode.Response
{
    // For Get Code Details By Item Code Response
    public class GetCodeDetailsResponseDto
    {
        public int CodeID { get; set; }
        public string CodeName { get; set; }
        public string CodeNameAr { get; set; }
        public string Description { get; set; }
        public string DescriptionAr { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public string ParentCodeLookupValue { get; set; }
        public int CodeTypeID { get; set; }
        public int CodeTypeLevelID { get; set; }
        public string CodeTypeLevelNamePrimaryLang { get; set; }
        public string CodeTypeLevelNameSecondaryLang { get; set; }
        public string ParentItemCode { get; set; }
        public int ParentCodeID { get; set; }
        public string ParentCodeName { get; set; }
        public string ParentCodeNameAr { get; set; }
        public string ParentLevelName { get; set; }
        public DateTime ParentActiveFrom { get; set; }
        public DateTime ParentActiveTo { get; set; }
        public string ParentDescription { get; set; }
        public string ParentDescriptionAr { get; set; }
        public bool ParentActive { get; set; }
        public bool Active { get; set; }
    }


}
