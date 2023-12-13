namespace AI_Social_Platform.Services.Data.Models.SocialFeature
{
    public class NotificationDto
    {
        public Guid Id { get; set; }

        public Guid ReceivingUserId { get; set; }

        public Guid CreatingUserId { get; set; }

        public string Content { get; set; } = null!;

        public string RedirectUrl { get; set; } = null!;

        public string NotificationType { get; set; } = null!;

        public DateTime DateCreated { get; set; } 
    }
}
