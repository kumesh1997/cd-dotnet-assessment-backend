using AutoMapper;
using DataAccess.DBContexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Dtos;
using static Core.Exceptions.DomainException;

namespace DataAccess.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ClassRepository> _logger;

        public ClassRepository(SchoolDbContext dbContext, IMapper mapper, ILogger<ClassRepository> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _logger.LogInformation("Ctor Assessment.DataAccess.Repositories.ClassRepository");
        }

        public async Task<Class> CreateClass(CreateClassDto createClassDto)
        {
            _logger.LogError("Assessment.DataAccess.Repositories.ClassRepository.CreateClass | Method in progress. Class Name: {ClassName}", createClassDto.ClassName);
            Class newClass = _mapper.Map<Class>(createClassDto);
            await _dbContext.Classes.AddAsync(newClass);
            await _dbContext.SaveChangesAsync();
            return newClass;
        }

        public async Task<bool> DeleteClass(int classId)
        {
            Class classEntity = await _dbContext.Classes.FindAsync(classId);

            if (classEntity == null)
            {
                return false;
            }

            classEntity.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Class>> GetAllClasses()
        {
            return await _dbContext.Classes
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Class> GetClassById(int classId)
        {
            return await _dbContext.Classes
                .FirstOrDefaultAsync(c => c.ClassId == classId && !c.IsDeleted);
        }

        public async Task<List<Student>> GetAllClassStudents(int classId)
        {
            var classEntity = await _dbContext.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.ClassId == classId && !c.IsDeleted);

            if (classEntity == null)
            {
                throw new ResourceNotFoundException($"Class with ID {classId} not found.");
            }

            return classEntity.Students.ToList();
        }

        public async Task<Class> UpdateClass(int classId, UpdateClassDto updateClassDto)
        {
            Class classEntity = await _dbContext.Classes.FindAsync(classId);
            if (classEntity == null)
            {
                throw new ResourceNotFoundException($"Class with ID {classId} not found.");
            }

            classEntity.ClassName = updateClassDto.className ?? classEntity.ClassName;
            classEntity.Grade = updateClassDto.grade ?? classEntity.Grade;
            classEntity.TeacherId = updateClassDto.teacherId ?? classEntity.TeacherId;

            await _dbContext.SaveChangesAsync();

            return classEntity;
        }
    }
}
