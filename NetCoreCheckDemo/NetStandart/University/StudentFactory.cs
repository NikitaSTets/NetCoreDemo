using NetCoreCheckDemo.University;

namespace NetStandart.University
{
    public class StudentFactory : IStudentFactory
    {
        public Student CreateDefaultStudent()
        {
            var defaultStudent = new Student("Roma", "Roma");

            return defaultStudent;
        }
    }
}