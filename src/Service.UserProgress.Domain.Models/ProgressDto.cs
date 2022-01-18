using Service.Core.Domain.Models.Education;

namespace Service.UserProgress.Domain.Models
{
	public class ProgressDto
	{
		public ProgressDto() => Tutorial = EducationTutorial.None;

		public EducationTutorial Tutorial { get; set; }

		public int TaskCount { get; set; }

		public int Progress { get; set; }
	}
}