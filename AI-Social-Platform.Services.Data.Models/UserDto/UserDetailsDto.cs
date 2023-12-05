namespace AI_Social_Platform.Services.Data.Models.UserDto
{
    using PublicationDtos;

    public class UserDetailsDto : UserDto
    {
        public string Email { get; set; } = null!;

        public string? CoverPhotoBase64 { get; set; }

        public string? Country { get; set; }

        public string? PhoneNumber { get; set; }

        public string? State { get; set; }

        public string? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string? Relationship { get; set; }
        
        public List<FriendDetailsDto> Friends { get; set; } = new List<FriendDetailsDto>();
        public List<PublicationDto> Publications { get; set; } = new List<PublicationDto>();
        public List<SchoolDto> UserSchools { get; set; } = new List<SchoolDto>();

    }
}
