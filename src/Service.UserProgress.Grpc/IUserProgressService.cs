using System.ServiceModel;
using System.Threading.Tasks;
using Service.UserProgress.Grpc.Models;

namespace Service.UserProgress.Grpc
{
	[ServiceContract]
	public interface IUserProgressService
	{
		[OperationContract]
		ValueTask<UnitedProgressGrpcResponse> GetUnitedProgressAsync(GetProgressGrpcRequset request);

		#region Knowledge

		[OperationContract]
		ValueTask<ProgressGrpcResponse> GetKnowledgeProgressAsync(GetProgressGrpcRequset request);

		[OperationContract]
		ValueTask<AllProgressGrpcResponse> GetAllKnowledgeProgressAsync(GetAllProgressGrpcRequset request);

		#endregion

		#region Habit

		[OperationContract]
		ValueTask<ProgressGrpcResponse> GetHabitProgressAsync(GetProgressGrpcRequset request);

		[OperationContract]
		ValueTask<AllProgressGrpcResponse> GetAllHabitProgressAsync(GetAllProgressGrpcRequset request);

		#endregion

		#region Skill

		[OperationContract]
		ValueTask<ProgressGrpcResponse> GetSkillProgressAsync(GetProgressGrpcRequset request);

		[OperationContract]
		ValueTask<AllProgressGrpcResponse> GetAllSkillProgressAsync(GetAllProgressGrpcRequset request);

		#endregion
	}
}