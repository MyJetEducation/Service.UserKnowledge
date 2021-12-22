using System;
using System.Runtime.Serialization;
using Service.Core.Domain.Models.Constants;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class SetKnowledgeLevelGrpcRequset
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public Tutorial Level { get; set; }
	}
}