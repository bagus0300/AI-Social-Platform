namespace AI_Social_Platform.FormModels
{
    using System.ComponentModel.DataAnnotations;

    public class OpenAiRequestFormModel
    {
        [Required]
        public string Prompt { get; set; } = null!;
    }
}
