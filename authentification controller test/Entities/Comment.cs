namespace authentification_controller_test
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string comment { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}
