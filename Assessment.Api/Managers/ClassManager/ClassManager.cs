using AutoMapper;
using Azure;
using DataAccess.Entities;
using DataAccess.Repositories;
using Model.Dtos;
using Model.Responses;
using static Core.Exceptions.DomainException;

namespace Assessment.Api.Managers
{
    public class ClassManager : IClassManager
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClassManager> _logger;

        public ClassManager(IClassRepository classRepository, IMapper mapper, ILogger<ClassManager> logger) 
        {
            _classRepository = classRepository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogInformation("Ctor Assessment.Api.Managers.ClassManager");
        }

        public async Task<BaseResponse<Class>> CreateClass(CreateClassDto createClassDto)
        {
            try
            {
                Class newClass = _mapper.Map<Class>(createClassDto);

                Class response = await _classRepository.CreateClass(createClassDto);

                if (response == null) 
                {
                    _logger.LogError("Assessment.Api.Managers.ClassManager.CreateClass | Class not created | Class entity: {createClassDto}.", createClassDto);
                    throw new ResourceNotFoundException("CLASS_NOT_CREATED");
                }
                
                return new BaseResponse<Class> { Succeeded = true , Data = response , Message = "CLASS_CREATED" };
            }
            catch (Exception ex) {
                throw;
            }
        }

        public async Task<BaseResponse<bool>> DeleteClass(int classId)
        {
            try
            {
                bool response = await _classRepository.DeleteClass(classId);
                if (response == false)
                {
                    _logger.LogError("Assessment.Api.Managers.ClassManager.DeleteClass | Class not founs | Class ID: {classId}.", classId);
                    throw new ResourceNotFoundException("NO_CLASS_FOUND");
                }
                return new BaseResponse<bool> { Succeeded = true, Data = response, Message = "CLASS_DELETED" };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<BaseResponse<List<Class>>> GetAllClasses()
        {
            try
            {
                List<Class> classes = await _classRepository.GetAllClasses();
                if (classes == null)
                {
                    _logger.LogError("Assessment.Api.Managers.ClassManager.GetAllClasses | Classes not found .");
                    throw new ResourceNotFoundException("NO_CLASSES_FOUND");
                }
                return new BaseResponse<List<Class>> { Succeeded = true, Data = classes, Message = "CLASSES_RETRIVED" };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BaseResponse<Class>> GetClassById(int classId)
        {
            try
            {
                Class classes = await _classRepository.GetClassById(classId);
                if (classes == null)
                {
                    _logger.LogError("Assessment.Api.Managers.ClassManager.GetClassById | Class not found | Class ID: {classId}.", classId);
                    throw new ResourceNotFoundException("NO_CLASS_FOUND");
                }
                return new BaseResponse<Class> { Succeeded = true, Data = classes, Message = "CLASSES_RETRIVED" };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BaseResponse<List<Student>>> GetAllClassStudents(int classId)
        {
            try
            {
                var students = await _classRepository.GetAllClassStudents(classId);

                if (students == null || students.Count == 0)
                {
                    _logger.LogError("Assessment.Api.Managers.ClassManager.GetAllClassStudents | Students not found | Class ID: {classId}.", classId);
                    throw new ResourceNotFoundException("NO_STUDENT_FOUND");
                }

                return new BaseResponse<List<Student>> { Succeeded = true, Data = students, Message = "DATA_RETRIVED"};
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BaseResponse<Class>> UpdateClass(int classId, UpdateClassDto updateClassDto)
        {
            try
            {
                Class response = await _classRepository.UpdateClass(classId, updateClassDto);
                if (response == null)
                {
                    _logger.LogError("Assessment.Api.Managers.ClassManager.UpdateClass | Class not updated | Class ID: {classId} | Class entity: {updateClassDto}.", classId, updateClassDto);
                    throw new ResourceNotFoundException("CLASS_NOT_UPDATED");
                }
                return new BaseResponse<Class> { Succeeded = true, Data = response, Message = "CLASSES_UPDATED" };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
