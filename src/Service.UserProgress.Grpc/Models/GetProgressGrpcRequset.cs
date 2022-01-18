using System;
using System.Runtime.Serialization;

namespace Service.UserProgress.Grpc.Models
{
	[DataContract]
	public class GetProgressGrpcRequset
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }
	}
}