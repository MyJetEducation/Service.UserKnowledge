using System.Runtime.Serialization;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class KnowledgeProgressGrpcResponse
	{
		[DataMember(Order = 1)]
		public int Progress { get; set; }
	}
}