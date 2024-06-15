namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.SearchPublishedCodes.Request
{
    public class PublishedCodeDto
    {
        public int CodeID { get; set; }
        public string CodeLookupValue { get; set; }
        public string CodeNamePrimaryLang { get; set; }
        public string CodeNameSecondaryLang { get; set; }
        public string CodeDescriptionPrimaryLang { get; set; }
        public string CodeDescriptionSecondaryLang { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public int ParentCodeID { get; set; }
        public string ParentCodeLookupValue { get; set; }
        public int CodeTypeID { get; set; }
        public int CodeTypeLevelID { get; set; }
        public string CodeTypeLevelNamePrimaryLang { get; set; }
        public string CodeTypeLevelNameSecondaryLang { get; set; }
        public string ParentCodeNamePrimaryLang { get; set; }
        public string ParentCodeNameSecondaryLang { get; set; }
        public string ParentLevelName { get; set; }
        public string CodeTypeNamePrimaryLang { get; set; }
        public bool Active { get; set; }
        public string LinkedCode { get; set; }
    }


}
