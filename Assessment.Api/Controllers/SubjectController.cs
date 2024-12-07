using Assessment.Api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Api.Controllers
{
    [ApiController]
    public class SubjectController : CoreController
    {
        private readonly ISubjectManager _subjectManager;
        private readonly ILogger<SubjectController> _logger;
        
        public SubjectController( ISubjectManager subjectManager ,ILogger<SubjectController> logger)
        {
            _subjectManager = subjectManager;
            _logger = logger;
        }
    }
}
