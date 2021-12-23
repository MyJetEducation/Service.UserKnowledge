using System.Runtime.Serialization;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class KnowledgeGrpcResponse
	{
		[DataMember(Order = 1)]
		public KnowledgeGrpcModel Knowledge { get; set; }
	}
}