using AutoMapper;
using Azure;
using DataAccess.DBContexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Dtos;
using Model.Requests;
using System.Globalization;
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

        public async Task<Class> Add(CreateClassDto createClassDto)
        {
            _logger.LogError("Assessment.DataAccess.Repositories.ClassRepository.CreateClass | Method in progress. Class Name: {ClassName}", createClassDto.ClassName);
            Class newClass = _mapper.Map<Class>(createClassDto);
            await _dbContext.Classes.AddAsync(newClass);
            await _dbContext.SaveChangesAsync();
            return newClass;
        }

        public async Task<bool> Delete(int classId)
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

        public async Task<List<Class>> GetAll()
        {
            return await _dbContext.Classes
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Class> GetById(int classId)
        {
            return await _dbContext.Classes
                .FirstOrDefaultAsync(c => c.ClassId == classId && !c.IsDeleted);
        }

        public IQueryable<Class> GetPaginatedList(ClassPaginatedRequest request)
        {
            // Extract and normalize filters
            string? className = request.ClassName?.Trim().ToLower() ?? string.Empty;
            string? grade = request.Grade?.Trim().ToLower() ?? string.Empty;
            string? teacherId = request.TeacherId?.Trim() ?? string.Empty;

            // Start with the base query
            IQueryable<Class> classes = _dbContext.Classes
                .Where(c =>
                    (string.IsNullOrEmpty(className) || c.ClassName.ToLower().Contains(className)) &&
                    (string.IsNullOrEmpty(grade) || c.Grade.ToLower() == grade) &&
                    (string.IsNullOrEmpty(teacherId) || c.TeacherId == teacherId) &&
                    (c.IsDeleted == false));

            if (!string.IsNullOrWhiteSpace(request.sortBy))
            {
                switch (request.sortBy.ToLower())
                {
                    case "classname":
                        classes = request.accending ? classes.OrderBy(c => c.ClassName) : classes.OrderByDescending(c => c.ClassName);
                        break;
                    case "grade":
                        classes = request.accending ? classes.OrderBy(c => c.Grade) : classes.OrderByDescending(c => c.Grade);
                        break;
                    case "teacherid":
                        classes = request.accending ? classes.OrderBy(c => c.TeacherId) : classes.OrderByDescending(c => c.TeacherId);
                        break;
                    default:
                        classes = classes.OrderBy(c => c.ClassId);
                        break;
                }
            }

            if (request.limit > 0)
            {
                classes = classes
                    .Skip((request.page - 1) * request.limit)
                    .Take(request.limit);
            }

            return classes.AsQueryable();
        }

        public async Task<ClassDto> GetStudentsByClassId(int classId)
        {
            var classEntity = await _dbContext.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.ClassId == classId && !c.IsDeleted);

            if (classEntity == null)
            {
                return null;
            }

            ClassDto classDto = new ClassDto
            {
                ClassId = classEntity.ClassId,
                ClassName = classEntity.ClassName,
                Grade = classEntity.Grade,
                Students = classEntity.Students
        .Select(s => new StudentDto { StudentId = s.StudentId, StudentName = s.Name })
        .ToList()
            };
            return classDto;
        }

        public async Task<Class> Update(int classId, UpdateClassDto updateClassDto)
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
