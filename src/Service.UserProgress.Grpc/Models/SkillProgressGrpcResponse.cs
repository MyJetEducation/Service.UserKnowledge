using System.Runtime.Serialization;

namespace Service.UserProgress.Grpc.Models
{
	[DataContract]
	public class SkillProgressGrpcResponse
	{
		[DataMember(Order = 1)]
		public int Total { get; set; }

		[DataMember(Order = 2)]
		public int Concentration { get; set; }

		[DataMember(Order = 3)]
		public int Perseverance { get; set; }

		[DataMember(Order = 4)]
		public int Thoughtfulness { get; set; }

		[DataMember(Order = 5)]
		public int Memory { get; set; }

		[DataMember(Order = 6)]
		public int Adaptability { get; set; }

		[DataMember(Order = 7)]
		public int Activity { get; set; }
	}
}