namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Notifications.Response
{
    public class NotificationDto
    {
        public string NotificationId { get; set; }
        public DateTime ReceivedDateTime { get; set; }
        public DateTime? DeliveredDateTime { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string FinalMessage { get; set; }
        public string Channel { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public DeliveryAttemptDto[] DeliveryAttempts { get; set; }
    }

}
