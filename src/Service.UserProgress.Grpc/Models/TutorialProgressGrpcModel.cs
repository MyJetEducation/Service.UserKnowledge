using System.Runtime.Serialization;

namespace Service.UserProgress.Grpc.Models
{
	[DataContract]
	public class TutorialProgressGrpcModel
	{
		[DataMember(Order = 1)]
		public int Index { get; set; }

		[DataMember(Order = 2)]
		public int Progress { get; set; }
	}
}