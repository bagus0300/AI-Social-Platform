namespace AI_Social_Platform.FormModels
{
    public class LoginResponse
    {
        public string? Token { get; set; }

        public bool Succeeded { get; set; }

        public string? UserId { get; set; }

        public string? Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? ErrorMessage { get; set; }

        public byte[]? ProfilePictureData { get; set; }

        public string? ProfilePicture { get; set; }
    }
}