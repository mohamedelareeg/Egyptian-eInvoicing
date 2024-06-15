namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Notifications.Response
{
    public class DeliveryAttemptDto
    {
        public DateTime AttemptDateTime { get; set; }
        public string Status { get; set; }
        public string StatusDetails { get; set; }
    }

}
