namespace ABCLearn.Models
{
    public class Comment
    {
        public int IDStudent { get; set; }
        public int IDCourse { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
