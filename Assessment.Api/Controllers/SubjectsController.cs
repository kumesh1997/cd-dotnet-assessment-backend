using Assessment.Api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Api.Controllers
{
    [ApiController]
    public class SubjectsController : CoreController
    {
        private readonly ISubjectManager _subjectManager;
        private readonly ILogger<SubjectsController> _logger;
        
        public SubjectsController( ISubjectManager subjectManager ,ILogger<SubjectsController> logger)
        {
            _subjectManager = subjectManager;
            _logger = logger;
        }
    }
}
