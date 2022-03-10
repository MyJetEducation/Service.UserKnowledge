using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Service.UserProgress.Models
{
	public class SkillProgressDto
	{
		public SkillProgressDto()
		{
			ConcentrationProgress = new List<int>();
			PerseveranceProgress = new List<int>();
			ThoughtfulnessProgress = new List<int>();
			MemoryProgress = new List<int>();
			AdaptabilityProgress = new List<int>();
			ActivityProgress = new List<int>();
		}

		public List<int> ConcentrationProgress { get; set; }

		public List<int> PerseveranceProgress { get; set; }

		public List<int> ThoughtfulnessProgress { get; set; }

		public List<int> MemoryProgress { get; set; }

		public List<int> AdaptabilityProgress { get; set; }

		public List<int> ActivityProgress { get; set; }

		[JsonIgnore]
		public int[] TotalProgress => ConcentrationProgress
			.Concat(PerseveranceProgress)
			.Concat(ThoughtfulnessProgress)
			.Concat(MemoryProgress)
			.Concat(AdaptabilityProgress)
			.Concat(ActivityProgress)
			.ToArray();
	}
}