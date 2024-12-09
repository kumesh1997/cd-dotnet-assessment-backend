using DataAccess.Entities;
using Model.Dtos;
using Model.Requests;
using Model.Responses;

namespace Assessment.Api.Managers
{
    public interface IClassManager
    {
        Task<BaseResponse<Class>> AddClass(CreateClassDto createClassDto);
        Task<BaseResponse<bool>> DeleteClass(int classId);
        Task<BaseResponse<List<Class>>> GetAllClasses();
        Task<BaseResponse<Class>> GetClassById(int classId);
        Task<BaseResponse<Class>> UpdateClass(int classId, UpdateClassDto updateClassDto);
        Task<BaseResponse<ClassDto>> GetStudentsInClass(int classId);
        Task<BaseResponse<Result<Class>>> GetPaginatedList(ClassPaginatedRequest classPaginatedRequest);
    }
}
