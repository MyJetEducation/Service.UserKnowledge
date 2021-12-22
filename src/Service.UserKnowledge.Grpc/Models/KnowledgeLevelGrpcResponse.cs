using System.Runtime.Serialization;
using Service.UserKnowledge.Domain.Models;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class KnowledgeLevelGrpcResponse
	{
		[DataMember(Order = 1)]
		public KnowledgeLevel? Level { get; set; }
	}
}