using Assessment.Api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Api.Controllers
{
    [ApiController]
    public class StudentController : CoreController
    {
        private readonly IStudentManager _studentManager;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentManager studentManager, ILogger<StudentController> logger) 
        {
            _studentManager = studentManager;
            _logger = logger;
        }

    }
}
