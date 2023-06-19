namespace ABCLearn.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int IdStudent { get; set; }
        public string CourseTitle { get; set; }
        public double Price { get; set; }
        public DateTime Created { get; set; }
        public string Method { get; set; }
        public string OrderID { get; set; }
    }
}
