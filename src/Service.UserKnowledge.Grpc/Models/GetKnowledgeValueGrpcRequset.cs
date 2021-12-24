using System;
using System.Runtime.Serialization;
using Service.Core.Domain.Models.Education;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class GetKnowledgeValueGrpcRequset
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public EducationTutorial Tutorial { get; set; }
	}
}