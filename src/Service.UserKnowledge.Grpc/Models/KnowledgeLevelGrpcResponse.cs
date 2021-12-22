using System.Runtime.Serialization;
using Service.Core.Domain.Models.Constants;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class KnowledgeLevelGrpcResponse
	{
		[DataMember(Order = 1)]
		public Tutorial? Level { get; set; }
	}
}