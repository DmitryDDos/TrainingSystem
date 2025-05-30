using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Services;

public class CourseRegistrationService : ICourseRegistrationService
{
    private readonly ICourseRegistrationRepository _registrationRepo;
    private readonly ICourseRepository _courseRepo;
    private readonly IUserProgressRepository _progressRepo;

    public CourseRegistrationService(
        ICourseRegistrationRepository registrationRepo,
        ICourseRepository courseRepo,
        IUserProgressRepository progressRepo)
    {
        _registrationRepo = registrationRepo;
        _courseRepo = courseRepo;
        _progressRepo = progressRepo;
    }

    public async Task<RegistrationResponseDto> RegisterAsync(int userId, RegistrationRequestDto request)
    {
        var registration = await _registrationRepo.RegisterUserAsync(userId, request.CourseId);
        var course = await _courseRepo.GetByIdAsync(request.CourseId);

        return new RegistrationResponseDto(
            registration.Id,
            course!.Id,
            course.Title,
            registration.Date
        );
    }

    public async Task<AccessCheckDto> CheckAccessAsync(int userId, int courseId)
        => new(await _registrationRepo.HasAccessAsync(userId, courseId));

    public async Task<IEnumerable<UserCourseDto>> GetUserCoursesAsync(int userId)
    {
        var courses = await _registrationRepo.GetUserCoursesAsync(userId);
        var result = new List<UserCourseDto>();

        foreach (var course in courses)
        {
            int totalModules = course.Modules?.Count ?? 0;
            int completedModules = (await _progressRepo.GetByUserAndCourseAsync(userId, course.Id))?.CompletedModules ?? 0;

            result.Add(new UserCourseDto(
                course.Id,
                course.Title,
                course.Description ?? "Описание отсутствует",
                course.Registrations.FirstOrDefault(r => r.UserId == userId)?.Date ?? DateOnly.MinValue,
                totalModules,
                completedModules
            ));
        }
        return result;
    }

    public async Task<int> GetRegistrationCountAsync(int courseId)
        => await _registrationRepo.GetRegistrationCountAsync(courseId);
}
