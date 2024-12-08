using DataAccess.Entities;
using Model.Dtos;
using Model.Responses;

namespace Assessment.Api.Managers
{
    public interface IClassManager
    {
        Task<BaseResponse<Class>> CreateClass(CreateClassDto createClassDto);
        Task<BaseResponse<bool>> DeleteClass(int classId);
        Task<BaseResponse<List<Class>>> GetAllClasses();
        Task<BaseResponse<Class>> GetClassById(int classId);
        Task<BaseResponse<Class>> UpdateClass(int classId, UpdateClassDto updateClassDto);
        Task<BaseResponse<ClassDto>> GetAllClassStudents(int classId);
    }
}
