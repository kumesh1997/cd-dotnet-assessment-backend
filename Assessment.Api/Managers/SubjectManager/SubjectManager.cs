using DataAccess.Repositories;

namespace Assessment.Api.Managers
{
    public class SubjectManager : ISubjectManager
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectManager(ISubjectRepository subjectRepository) 
        {
            _subjectRepository = subjectRepository;
        }
    }
}
