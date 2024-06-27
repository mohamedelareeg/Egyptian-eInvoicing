using System.Text.Json.Serialization;

namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details
{
    public class AddressDto
    {
        public string? BranchID { get; set; } = "0";
        public string? Country { get; set; } = "EG";
        public string? Governate { get; set; } = "";
        public string? RegionCity { get; set; } = "";
        public string? Street { get; set; } = "";
        public string? BuildingNumber { get; set; } = "";
        public string? PostalCode { get; set; } = "";
        public string? Floor { get; set; } = "";
        public string? Room { get; set; } = "";
        public string? Landmark { get; set; } = "";
        public string? AdditionalInformation { get; set; } = "";
    }
}
