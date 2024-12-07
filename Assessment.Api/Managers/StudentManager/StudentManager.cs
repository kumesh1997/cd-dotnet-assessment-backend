using DataAccess.Repositories;

namespace Assessment.Api.Managers
{
    public class StudentManager : IStudentManager
    {
        private readonly IStudentRepository _studentRepository;
        public StudentManager(IStudentRepository studentRepository) 
        {
            _studentRepository = studentRepository;
        }
    }
}
