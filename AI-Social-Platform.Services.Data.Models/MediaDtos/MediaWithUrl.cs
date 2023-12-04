namespace AI_Social_Platform.Services.Data.Models.MediaDtos
{
    using System;
   

    public class MediaWithUrl
    {
        public Guid FileId { get; set; }
        public string? FileName { get; set; }
        public string? Url { get; set; }
    }
}
