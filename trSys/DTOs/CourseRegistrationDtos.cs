using System.ComponentModel.DataAnnotations;

namespace trSys.DTOs;

public record RegistrationRequestDto
{
    [Required]
    public int CourseId { get; init; }
}


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

