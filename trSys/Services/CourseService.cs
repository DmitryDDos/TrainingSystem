using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;
using trSys.Mappers;

namespace trSys.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IModuleRepository _moduleRepo;

        public CourseService(
            ICourseRepository courseRepo,
            IModuleRepository moduleRepo)
        {
            _courseRepo = courseRepo;
            _moduleRepo = moduleRepo;
        }

        public async Task<CourseDto> CreateCourseAsync(CourseCreateDto dto)
        {
            if (await _courseRepo.TitleExistsAsync(dto.Title))
                throw new ArgumentException("Course title already exists");

            var course = new Course(dto.Title, dto.Description); // Теперь работает
            await _courseRepo.AddAsync(course);

            return CourseMapper.ToDto(course);
        }

        public async Task<CourseDetailsDto> GetCourseDetailsAsync(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null) return null;

            var modules = await _moduleRepo.GetByCourseIdAsync(id);

            return CourseMapper.ToDetailsDto(course, modules);
        }
    }
}
