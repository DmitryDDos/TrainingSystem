namespace trSys.DTOs;

public record UserProgressDto(
    int UserId,
    int CourseId,
    int CompletedModules,
    int TotalModules
)
{
    public double ProgressPercentage =>
        TotalModules == 0 ? 0 : (CompletedModules * 100.0) / TotalModules;
}
