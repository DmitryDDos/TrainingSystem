using trSys.DTOs;

namespace trSys.Models { 
public class CourseLearningVM
    {
        public Course Course { get; set; }
        public Module CurrentModule { get; set; }
        public UserProgressDto Progress { get; set; }
        public bool IsCourseCompleted { get; set; }
    }
} //namespace trSys.Models