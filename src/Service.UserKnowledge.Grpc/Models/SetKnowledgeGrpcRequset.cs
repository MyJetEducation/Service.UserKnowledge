using System;
using System.Runtime.Serialization;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class SetKnowledgeGrpcRequset
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public KnowledgeGrpcModel Knowledge { get; set; }
	}
}