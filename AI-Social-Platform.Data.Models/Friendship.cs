namespace AI_Social_Platform.Data.Models
{
    public class Friendship
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }

        public ApplicationUser User { get; set; }
        public ApplicationUser Friend { get; set; }
    }
}
