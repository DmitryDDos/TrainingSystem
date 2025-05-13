using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Services;

public class CourseRegistrationService : ICourseRegistrationService
{
    private readonly ICourseRegistrationRepository _registrationRepo;
    private readonly ICourseRepository _courseRepo;

    public CourseRegistrationService(
        ICourseRegistrationRepository registrationRepo,
        ICourseRepository courseRepo)
    {
        _registrationRepo = registrationRepo;
        _courseRepo = courseRepo;
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
        => (await _registrationRepo.GetUserCoursesAsync(userId))
            .Select(c => new UserCourseDto(
                c.Id,
                c.Title,
                c.Description ?? "Описание отсутствует",
                c.Registrations.FirstOrDefault(r => r.UserId == userId)?.Date ?? DateOnly.MinValue
            ));

    public async Task<int> GetRegistrationCountAsync(int courseId)
        => await _registrationRepo.GetRegistrationCountAsync(courseId);
}
