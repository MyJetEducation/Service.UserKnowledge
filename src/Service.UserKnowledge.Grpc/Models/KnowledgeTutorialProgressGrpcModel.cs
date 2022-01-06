using System.Runtime.Serialization;
using Service.Core.Domain.Models.Education;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class KnowledgeTutorialProgressGrpcModel
	{
		[DataMember(Order = 1)]
		public EducationTutorial Tutorial { get; set; }

		[DataMember(Order = 2)]
		public int Progress { get; set; }
	}
}