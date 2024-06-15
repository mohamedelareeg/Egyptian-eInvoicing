namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentNotification.Request
{
    public class DocumentNotificationMessageDto
    {
        public string type { get; set; }
        public string uuid { get; set; }
        public string submissionUUID { get; set; }
        public string longId { get; set; }
        public string internalId { get; set; }
        public string status { get; set; }
    }
}
