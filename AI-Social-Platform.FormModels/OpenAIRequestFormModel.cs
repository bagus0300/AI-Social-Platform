namespace AI_Social_Platform.FormModels
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models.Enums;

    public class OpenAiRequestFormModel
    {
        [Required]
        [Display(Name = "Select a Subject")]
        public string Subject { get; set; } = null!;

        [Required]
        [Display(Name = "Type of Audience")]
        public OpenAiAudienceType Audience { get; set; }

        [Required]
        [Display(Name = "Type of Tone")]
        public FormatTone Tone { get; set; }

        [Required]
        [Display(Name = "Text Length")]
        public TextLength TextLength { get; set; }
    }
}
