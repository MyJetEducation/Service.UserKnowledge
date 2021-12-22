namespace Service.UserKnowledge.Domain.Models
{
	public class KnowledgeLevelDto
	{
		public KnowledgeLevelDto(KnowledgeLevel level) => Level = level;

		public KnowledgeLevel Level { get; set; }
	}
}