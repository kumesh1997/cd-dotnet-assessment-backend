using Assessment.Api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Api.Controllers
{
    [ApiController]
    public class ClassController : CoreController
    {
        private readonly IClassManager _classManager;
        private readonly ILogger<ClassController> _logger;
        public ClassController(IClassManager classManager, ILogger<ClassController> logger)
        {
            _classManager = classManager;
            _logger = logger;
        }
    }
}
