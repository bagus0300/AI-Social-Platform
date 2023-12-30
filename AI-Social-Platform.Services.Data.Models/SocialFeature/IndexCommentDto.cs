namespace AI_Social_Platform.Services.Data.Models.SocialFeature
{
    public class IndexCommentDto
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalComments { get; set; }

        public int CommentsLeft { get; set; }

        public IEnumerable<CommentDto> Comments { get; set; } = null!;
    }
}
