using AutoMapper;
using Azure;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Model.Dtos;
using Model.Requests;
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

        public async Task<BaseResponse<Class>> AddClass(CreateClassDto createClassDto)
        {
            try
            {
                Class newClass = _mapper.Map<Class>(createClassDto);

                Class response = await _classRepository.Add(createClassDto);

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
                bool response = await _classRepository.Delete(classId);
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
                List<Class> classes = await _classRepository.GetAll();
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
                Class classes = await _classRepository.GetById(classId);
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

        public async Task<BaseResponse<Result<Class>>> GetPaginatedList(ClassPaginatedRequest classPaginatedRequest)
        {
            try
            {
                IQueryable<Class> query = _classRepository.GetPaginatedList(classPaginatedRequest);

                List<Class> paginatedList = await query.ToListAsync();

                int totalLength = await query.CountAsync();

                BaseResponse<Result<Class>> response = new BaseResponse<Result<Class>>
                {
                    Succeeded = true,
                    Data = new Result<Class>
                    {
                        Entities = paginatedList.ToArray(),
                        Pagination = new Pagination
                        {
                            Length = totalLength,
                            PageSize = classPaginatedRequest.limit
                        }
                    },
                    Message = "DATA_RETRIVED"

                };

                return response;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<BaseResponse<ClassDto>> GetStudentsInClass(int classId)
        {
            try
            {
                ClassDto response = await _classRepository.GetStudentsByClassId(classId);

                if (response == null || response.Students.Count == 0)
                {
                    _logger.LogError("Assessment.Api.Managers.ClassManager.GetAllClassStudents | Students not found | Class ID: {classId}.", classId);
                    throw new ResourceNotFoundException("NO_STUDENT_FOUND");
                }

                return new BaseResponse<ClassDto> { Succeeded = true, Data = response, Message = "DATA_RETRIVED"};
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
                Class response = await _classRepository.Update(classId, updateClassDto);
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
