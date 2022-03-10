using System.Text.Json.Serialization;

namespace Service.UserProgress.Models
{
	public class SkillProgressDto
	{
		public int ConcentrationCount { get; set; }

		public int PerseveranceCount { get; set; }

		public int ThoughtfulnessCount { get; set; }

		public int MemoryCount { get; set; }

		public int AdaptabilityCount { get; set; }

		public int ActivityCount { get; set; }

		[JsonIgnore]
		public int TotalCount => ConcentrationCount + PerseveranceCount + ThoughtfulnessCount + MemoryCount + AdaptabilityCount + ActivityCount;
	}
}