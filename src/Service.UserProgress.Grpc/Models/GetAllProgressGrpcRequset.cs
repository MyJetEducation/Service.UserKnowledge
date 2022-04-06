using System.Runtime.Serialization;

namespace Service.UserProgress.Grpc.Models
{
	[DataContract]
	public class GetAllProgressGrpcRequset
	{
		[DataMember(Order = 1)]
		public string UserId { get; set; }
	}
}