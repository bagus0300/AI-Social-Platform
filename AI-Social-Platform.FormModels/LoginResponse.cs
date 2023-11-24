namespace AI_Social_Platform.FormModels
{
    public class LoginResponse
    {
        public string? Token { get; set; }

        public bool Succeeded { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
