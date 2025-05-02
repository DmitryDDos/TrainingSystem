using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Services;

public class CourseService : ICourseRepository
{
    private readonly ICourseRepository _repository;

    public CourseService(ICourseRepository repository)
    {
        _repository = repository;
    }

    // Реализация ICourseRepository
    public async Task<IEnumerable<Course>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Course> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
    public async Task AddAsync(Course entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Course entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    public async Task<bool> CourseExists(int id) => await _repository.CourseExists(id);
    public async Task<IEnumerable<Module>> GetModulesByCourseId(int courseId) => await _repository.GetModulesByCourseId(courseId);

    // DTO методы
    public async Task<CourseDto> CreateFromDtoAsync(CourseCreateDto dto)
    {
        var course = new Course(0, dto.Title, dto.Description);
        await _repository.AddAsync(course);
        return ToDto(course);
    }

    private static CourseDto ToDto(Course course) => new()
    {
        Id = course.Id,
        Title = course.Title,
        Description = course.Descriptions
    };
}
