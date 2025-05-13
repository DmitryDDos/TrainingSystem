using trSys.DTOs;

namespace trSys.Interfaces;

public interface ICourseRegistrationService
{
    Task<RegistrationResponseDto> RegisterAsync(int userId, RegistrationRequestDto request);
    Task<AccessCheckDto> CheckAccessAsync(int userId, int courseId);
    Task<IEnumerable<UserCourseDto>> GetUserCoursesAsync(int userId);
    Task<int> GetRegistrationCountAsync(int courseId);
}
