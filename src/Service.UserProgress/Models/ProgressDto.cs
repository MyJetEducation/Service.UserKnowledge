using Service.Education.Structure;

namespace Service.UserProgress.Models
{
	public class ProgressDto
	{
		public ProgressDto() => Tutorial = EducationTutorial.None;

		public EducationTutorial Tutorial { get; set; }

		public int TaskCount { get; set; }

		public int Progress { get; set; }
	}
}