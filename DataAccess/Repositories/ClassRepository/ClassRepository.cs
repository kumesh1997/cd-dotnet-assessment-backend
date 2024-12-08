using AutoMapper;
using DataAccess.DBContexts;
using DataAccess.Entities;
using Model.Dtos;

namespace DataAccess.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;

        public ClassRepository(SchoolDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Class> CreateClass(CreateClassDto createClassDto)
        {
            Class newClass = _mapper.Map<Class>(createClassDto);
            _dbContext.Add(newClass);
            await _dbContext.SaveChangesAsync();
            return newClass;
        }
    }
}
