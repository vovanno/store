namespace OnlineStoreApi.CommentApi
{
    public class CreateCommentRequest
    {
        public string Body { get; set; }

        public int ProductRating { get; set; }

        public int ProductId { get; set; }

        public int? ParentId { get; set; }
    }

    public class EditCommentRequest
    {
        public string Body { get; set; }
    }
}
