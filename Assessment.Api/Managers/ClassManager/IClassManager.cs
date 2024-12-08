using DataAccess.Entities;
using Model.Dtos;
using Model.Responses;

namespace Assessment.Api.Managers
{
    public interface IClassManager
    {
        Task<BaseResponse<Class>> CreateClass(CreateClassDto createClassDto);
    }
}
