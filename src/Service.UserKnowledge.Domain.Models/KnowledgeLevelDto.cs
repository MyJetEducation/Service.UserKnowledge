using Service.Core.Domain.Models.Constants;

namespace Service.UserKnowledge.Domain.Models
{
	public class KnowledgeLevelDto
	{
		public KnowledgeLevelDto(Tutorial level) => Level = level;

		public Tutorial Level { get; set; }
	}
}