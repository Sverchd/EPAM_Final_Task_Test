namespace BusinessLogicLayer.Models
{
    public class Mark
    {
        public int MarkId { get; set; }
        public int CourseId { get; set; }
        public string StudentUsername { get; set; }
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
        public Mark(int course, string student, int? grade)
        {
            CourseId = course;
            StudentUsername = student;
            Grade = grade;
        }
    }
}