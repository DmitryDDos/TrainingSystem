using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs;

public record RegistrationRequestDto(
    [Required] int CourseId
// UserId берётся из JWT-токена, поэтому не включается в DTO
);

public record RegistrationResponseDto(
    int RegistrationId,
    int CourseId,
    string CourseTitle,
    DateOnly RegistrationDate
);

public record AccessCheckDto(
    bool HasAccess,
    string? ErrorMessage = null
);

public record UserCourseDto(
    int CourseId,
    string Title,
    string Description,
    DateOnly RegistrationDate
);

