using Service.UserKnowledge.Domain.Models;
using Service.UserKnowledge.Grpc.Models;

namespace Service.UserKnowledge.Mappers
{
	public static class KnowledgeMapper
	{
		public static KnowledgeDto ToDto(this KnowledgeGrpcModel grpcModel) => new KnowledgeDto
		{
			PersonalFinance = grpcModel.PersonalFinance,
			BehavioralFinance = grpcModel.BehavioralFinance,
			FinancialServices = grpcModel.FinancialServices,
			FinanceMarkets = grpcModel.FinanceMarkets,
			HealthAndFinance = grpcModel.HealthAndFinance,
			PsychologyAndFinance = grpcModel.PsychologyAndFinance,
			FinanceSecurity = grpcModel.FinanceSecurity,
			TimeManagement = grpcModel.TimeManagement,
			Economics = grpcModel.Economics
		};

		public static KnowledgeGrpcModel ToGrpcModel(this KnowledgeDto model) => new KnowledgeGrpcModel
		{
			PersonalFinance = model.PersonalFinance,
			BehavioralFinance = model.BehavioralFinance,
			FinancialServices = model.FinancialServices,
			FinanceMarkets = model.FinanceMarkets,
			HealthAndFinance = model.HealthAndFinance,
			PsychologyAndFinance = model.PsychologyAndFinance,
			FinanceSecurity = model.FinanceSecurity,
			TimeManagement = model.TimeManagement,
			Economics = model.Economics
		};
	}
}