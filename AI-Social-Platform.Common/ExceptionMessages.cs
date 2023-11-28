using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Social_Platform.Common
{
    public static class ExceptionMessages
    {
        public static class PublicationExceptionMessages
        {
            public const string PublicationNotFound = "Publication not found!";
            public const string PublicationAuthorNotFound = "Author not found!";
            public const string NotAuthorizedToEditPublication = "You are not authorized to edit this publication!";
            public const string NotAuthorizedToDeletePublication = "You are not authorized to delete this publication!";

            public const string CommentNotFound = "Comment not found!";
            public const string NotAuthorizedToEditComment = "You are not authorized to edit this comment!";
            public const string NotAuthorizedToDeleteComment = "You are not authorized to delete this comment!";

        }
    }
}
