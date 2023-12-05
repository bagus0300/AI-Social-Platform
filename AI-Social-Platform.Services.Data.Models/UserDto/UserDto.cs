namespace AI_Social_Platform.Services.Data.Models.UserDto
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? ProfilePictureBase64 { get; set; }
    }
}
