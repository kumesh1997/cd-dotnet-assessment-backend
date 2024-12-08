using Assessment.Api.Managers;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;
using Model.Responses;

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

        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassDto createClassDto)
        {
            BaseResponse<Class> createdClass = await _classManager.CreateClass(createClassDto);
            return Ok(createdClass);
        }
    }
}
