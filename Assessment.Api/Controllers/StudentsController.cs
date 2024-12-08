using Assessment.Api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Api.Controllers
{
    [ApiController]
    public class StudentsController : CoreController
    {
        private readonly IStudentManager _studentManager;
        private readonly ILogger<StudentsController> _logger;
        public StudentsController(IStudentManager studentManager, ILogger<StudentsController> logger) 
        {
            _studentManager = studentManager;
            _logger = logger;
        }

    }
}
