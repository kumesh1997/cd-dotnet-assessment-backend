using DataAccess.Entities;
using Model.Dtos;

namespace DataAccess.Repositories
{
    public interface IClassRepository
    {
        Task<Class> CreateClass(CreateClassDto createClassDto);
    }
}
