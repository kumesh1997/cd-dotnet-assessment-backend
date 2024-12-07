using DataAccess.Repositories;

namespace Assessment.Api.Managers
{
    public class ClassManager : IClassManager
    {
        private readonly IClassRepository _classRepository;
        public ClassManager(IClassRepository classRepository) 
        {
            _classRepository = classRepository;
        }
    }
}
