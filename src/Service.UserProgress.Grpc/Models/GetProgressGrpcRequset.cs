using System.Runtime.Serialization;

namespace Service.UserProgress.Grpc.Models
{
	[DataContract]
	public class GetProgressGrpcRequset
	{
		[DataMember(Order = 1)]
		public string UserId { get; set; }
	}
}