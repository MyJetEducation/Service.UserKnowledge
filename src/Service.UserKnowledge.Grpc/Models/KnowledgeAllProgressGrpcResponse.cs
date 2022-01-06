using System.Runtime.Serialization;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class KnowledgeAllProgressGrpcResponse
	{
		[DataMember(Order = 1)]
		public KnowledgeTutorialProgressGrpcModel[] Items { get; set; }
	}
}