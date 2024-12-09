using DataAccess.Entities;
using Model.Dtos;
using Model.Requests;

namespace DataAccess.Repositories
{
    public interface IClassRepository
    {
        Task<List<Class>> GetAll();
        Task<Class> GetById(int classId);
        Task<Class> Add(CreateClassDto createClassDto);
        Task<Class> Update(int classId, UpdateClassDto updateClassDto);
        Task<bool> Delete(int classId);
        Task<ClassDto> GetStudentsByClassId(int classId);
        IQueryable<Class> GetPaginatedList(ClassPaginatedRequest classPaginatedRequest);
    }
}
