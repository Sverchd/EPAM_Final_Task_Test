namespace BusinessLogicLayer.Models
{
    public class Mark
    {
        public int MarkId { get; set; }
        public Course Course { get; set; }
        public User Student { get; set; }
        public int? Grade { get; set; }

        /// <summary>
        ///     default constructor
        /// </summary>
        public Mark()
        {
        }

        /// <summary>
        ///     constructor
        /// </summary>
        /// <param name="course">course instance</param>
        /// <param name="student">student instance</param>
        /// <param name="grade">grade(mark)</param>
        public Mark(Course course, User student, int? grade)
        {
            Course = course;
            Student = student;
            Grade = grade;
        }
    }
}