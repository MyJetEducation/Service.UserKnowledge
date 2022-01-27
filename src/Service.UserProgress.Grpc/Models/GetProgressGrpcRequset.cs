using System;
using System.Runtime.Serialization;
using Service.Core.Client.Education;

namespace Service.UserProgress.Grpc.Models
{
	[DataContract]
	public class GetProgressGrpcRequset
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public EducationTutorial Tutorial { get; set; }
	}
}