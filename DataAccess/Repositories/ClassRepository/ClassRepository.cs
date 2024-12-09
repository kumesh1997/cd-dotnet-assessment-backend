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

        public IQueryable<Class> GetPaginatedList(ClassPaginatedRequest classPaginatedRequest)
        {
            int page = classPaginatedRequest.page;
            int pageSize = classPaginatedRequest.limit;
            string sortBy = classPaginatedRequest.sortBy;
            bool ascending = classPaginatedRequest.accending;

            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            IQueryable<Class> query = _dbContext.Classes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(classPaginatedRequest.ClassName))
            {
                query = query.Where(c => c.ClassName.Contains(classPaginatedRequest.ClassName));
            }
            if (!string.IsNullOrWhiteSpace(classPaginatedRequest.Grade))
            {
                query = query.Where(c => c.Grade == classPaginatedRequest.Grade);
            }
            if (!string.IsNullOrWhiteSpace(classPaginatedRequest.TeacherId))
            {
                query = query.Where(c => c.TeacherId == classPaginatedRequest.TeacherId);
            }
            if (classPaginatedRequest.IsDeleted.HasValue)
            {
                query = query.Where(c => c.IsDeleted == classPaginatedRequest.IsDeleted.Value);
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "classname":
                        query = ascending ? query.OrderBy(c => c.ClassName) : query.OrderByDescending(c => c.ClassName);
                        break;
                    case "grade":
                        query = ascending ? query.OrderBy(c => c.Grade) : query.OrderByDescending(c => c.Grade);
                        break;
                    case "teacherid":
                        query = ascending ? query.OrderBy(c => c.TeacherId) : query.OrderByDescending(c => c.TeacherId);
                        break;
                    default:
                        query = query.OrderBy(c => c.ClassId);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(c => c.ClassId);
            }

            IQueryable<Class> paginatedList = (IQueryable<Class>)query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return paginatedList.AsQueryable();
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
