using System.ComponentModel.DataAnnotations;
using static AI_Social_Platform.Common.EntityValidationConstants.Publication;

namespace AI_Social_Platform.Services.Data.Models.PublicationDtos
{
    public class PublicationFormDto
    {
        [StringLength(PublicationContentMaxLength, MinimumLength = 0)]
        public string Content { get; set; } = "";

        public Guid? TopicId { get; set; }
    }
}
