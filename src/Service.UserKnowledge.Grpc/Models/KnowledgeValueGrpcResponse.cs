using System.Runtime.Serialization;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class KnowledgeValueGrpcResponse
	{
		[DataMember(Order = 1)]
		public int? Value { get; set; }
	}
}