using Microsoft.AspNetCore.Http;
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

        // Основной метод
        public async Task<CourseDto> CreateCourseAsync(CourseCreateDto dto)
        {
            return await CreateCourseInternalAsync(dto, null);
        }

        // Дополнительный метод для контроллера с поддержкой файлов
        public async Task<CourseDto> CreateCourseWithFileAsync(CourseCreateDto dto, IFormFile coverImage)
        {
            return await CreateCourseInternalAsync(dto, coverImage);
        }

        private async Task<CourseDto> CreateCourseInternalAsync(CourseCreateDto dto, IFormFile coverImage)
        {
            if (await _courseRepo.TitleExistsAsync(dto.Title))
                throw new ArgumentException("Course title already exists");

            var course = new Course(dto.Title, dto.Description)
            {
                CoverImage = coverImage != null ? new FileEntity
                {
                    FileName = coverImage.FileName,
                    ContentType = coverImage.ContentType,
                    Data = await ReadFileBytesAsync(coverImage)
                } : null
            };

            await _courseRepo.AddAsync(course);
            return CourseMapper.ToDto(course);
        }

        private async Task<byte[]> ReadFileBytesAsync(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            return stream.ToArray();
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
