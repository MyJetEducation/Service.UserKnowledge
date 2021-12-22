using System;
using System.Runtime.Serialization;
using Service.UserKnowledge.Domain.Models;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class SetKnowledgeLevelGrpcRequset
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public KnowledgeLevel Level { get; set; }
	}
}