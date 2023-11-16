namespace AI_Social_Platform.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.MediaConstants;

    public class Media
    {
        public Media()
        {
                this.Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [MaxLength(MediaTitleMaxLength)]
        public string? Title { get; set; }

        public byte[] DataFile { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        public Guid UserId { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
