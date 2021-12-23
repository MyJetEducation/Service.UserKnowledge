using System.Runtime.Serialization;

namespace Service.UserKnowledge.Grpc.Models
{
	[DataContract]
	public class KnowledgeGrpcModel
	{
		[DataMember(Order = 1)]
		public int? PersonalFinance { get; set; }

		[DataMember(Order = 2)]
		public int? BehavioralFinance { get; set; }

		[DataMember(Order = 3)]
		public int? FinancialServices { get; set; }

		[DataMember(Order = 4)]
		public int? FinanceMarkets { get; set; }

		[DataMember(Order = 5)]
		public int? HealthAndFinance { get; set; }

		[DataMember(Order = 6)]
		public int? PsychologyAndFinance { get; set; }

		[DataMember(Order = 7)]
		public int? FinanceSecurity { get; set; }

		[DataMember(Order = 8)]
		public int? TimeManagement { get; set; }

		[DataMember(Order = 9)]
		public int? Economics { get; set; }
	}
}