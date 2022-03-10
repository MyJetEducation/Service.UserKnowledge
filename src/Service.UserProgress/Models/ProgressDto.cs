using System.Collections.Generic;
using Service.Education.Structure;

namespace Service.UserProgress.Models
{
	public class ProgressDto
	{
		public ProgressDto() => Tutorial = EducationTutorial.None;
		
		public ProgressDto(EducationTutorial tutorial)
		{
			Tutorial = tutorial;
			TaskProgress = new List<int>();
		}

		public EducationTutorial Tutorial { get; set; }

		public List<int> TaskProgress { get; set; }
		
		public int Progress { get; set; }
	}
}