using DataAccess.Entities;
using Model.Dtos;

namespace DataAccess.Repositories
{
    public interface IClassRepository
    {
        Task<List<Class>> GetAllClasses();
        Task<Class> GetClassById(int classId);
        Task<Class> CreateClass(CreateClassDto createClassDto);
        Task<Class> UpdateClass(int classId, UpdateClassDto updateClassDto);
        Task<bool> DeleteClass(int classId);
        Task<ClassDto> GetAllClassStudents(int classId);
    }
}
