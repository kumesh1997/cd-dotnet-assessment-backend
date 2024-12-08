using DataAccess.Entities;
using DataAccess.Repositories;
using Model.Dtos;
using Model.Responses;

namespace Assessment.Api.Managers
{
    public class ClassManager : IClassManager
    {
        private readonly IClassRepository _classRepository;
        public ClassManager(IClassRepository classRepository) 
        {
            _classRepository = classRepository;
        }

        public async Task<BaseResponse<Class>> CreateClass(CreateClassDto createClassDto)
        {
            try
            {

                Class response = await _classRepository.CreateClass(createClassDto);

                return new BaseResponse<Class> { Succeeded = true , Data = response , Message = "CLASS_CREATED" };
            }
            catch (Exception ex) {

                throw;
            }
        }
    }
}
