namespace AI_Social_Platform.Services.Data.Models.UserDto
{
    using PublicationDtos;

    public class UserDetailsDto : UserDto
    {
        public string Email { get; set; } = null!;

        public byte[]? CoverPhotoData { get; set; }

        public string? CoverPhotoUrl { get; set; }

        public string? Country { get; set; }

        public string? PhoneNumber { get; set; }

        public string? State { get; set; }

        public string? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string? Relationship { get; set; }

        public string? School { get; set; }
        
        public List<FriendDetailsDto> Friends { get; set; } = new List<FriendDetailsDto>();
        

    }
}
