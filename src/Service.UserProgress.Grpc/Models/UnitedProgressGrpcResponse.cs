using System.Runtime.Serialization;

namespace Service.UserProgress.Grpc.Models
{
	[DataContract]
	public class UnitedProgressGrpcResponse
	{
		[DataMember(Order = 1)]
		public ProgressGrpcResponse Knowledge { get; set; }

		[DataMember(Order = 2)]
		public ProgressGrpcResponse Habit { get; set; }

		[DataMember(Order = 3)]
		public ProgressGrpcResponse Skill { get; set; }
	}
}