using System.Runtime.Serialization;

namespace Service.UserProgress.Grpc.Models
{
	[DataContract]
	public class AllProgressGrpcResponse
	{
		[DataMember(Order = 1)]
		public TutorialProgressGrpcModel[] Items { get; set; }
	}
}