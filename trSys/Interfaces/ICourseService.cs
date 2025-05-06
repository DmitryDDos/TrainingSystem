using trSys.DTOs;

namespace trSys.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourseAsync(CourseCreateDto dto);
        Task<CourseDetailsDto> GetCourseDetailsAsync(int id);
    }
}
