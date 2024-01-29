namespace authentification_controller_test
{
    public class Comment
    {
        public int id { get; set; }
        public string comment { get; set; }
        public int AuthorId { get; set; }
        public virtual User author { get; set; }
    }
}
