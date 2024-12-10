using Assessment.Api.Managers;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;
using Model.Requests;
using Model.Responses;
using System.Text.Json;

namespace Assessment.Api.Controllers
{
    [ApiController]
    public class ClassesController : CoreController
    {
        private readonly IClassManager _classManager;
        private readonly ILogger<ClassesController> _logger;
        public ClassesController(IClassManager classManager, ILogger<ClassesController> logger)
        {
            _classManager = classManager;
            _logger = logger;
            _logger.LogInformation("Ctor Assessment.Api.Controllers.ClassesController");
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassDto createClassDto)
        {
            _logger.LogInformation("Assessment.Api.Controllers.ClassesController.CreateClass | Request in progress. | Request: {createClassDto}", JsonSerializer.Serialize(createClassDto));
            if (createClassDto == null) return BadRequest();
            BaseResponse<Class> createdClass = await _classManager.AddClass(createClassDto);
            return Ok(createdClass);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            _logger.LogInformation("Assessment.Api.Controllers.ClassesController.DeleteClass | Request in progress. | Class ID: {id}", id);
            BaseResponse<bool> response = await _classManager.DeleteClass(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClassess()
        {
            _logger.LogInformation("Assessment.Api.Controllers.ClassesController.GetAllClassess | Request in progress.");
            BaseResponse<List<Class>> response = await _classManager.GetAllClasses();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllClassessById(int id)
        {
            _logger.LogInformation("Assessment.Api.Controllers.ClassesController.GetAllClassessById | Request in progress. | Class ID: {id}", id);
            BaseResponse<Class> response = await _classManager.GetClassById(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(int id, UpdateClassDto updateClassDto)
        {
            _logger.LogInformation("Assessment.Api.Controllers.ClassesController.UpdateClass | Request in progress. | Class ID: {id} | Request: {updateClassDto}", id, JsonSerializer.Serialize(updateClassDto));
            if (updateClassDto == null || (id == 0 || id < 0) ) return BadRequest();
            BaseResponse<Class> response = await _classManager.UpdateClass(id, updateClassDto);
            return Ok(response);
        }

        [HttpGet("{id}/students")]
        public async Task<IActionResult> GetAllClassStudents(int id)
        {
            _logger.LogInformation("Assessment.Api.Controllers.ClassesController.GetAllClassStudents | Request in progress. | Class ID: {id}", id);
            BaseResponse<ClassDto> response = await _classManager.GetStudentsInClass(id);
            return Ok(response);
        }

        [HttpPost("/paginated")]
        public async Task<IActionResult> GetPaginated([FromBody] ClassPaginatedRequest request)
        {
            _logger.LogInformation("Assessment.Api.Controllers.ClassesController.GetPaginated | Request in progress. | Request: {request}", JsonSerializer.Serialize(request));
            return Ok(await _classManager.GetPaginatedList(request));
        }
    }
}
