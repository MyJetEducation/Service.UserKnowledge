using System.Threading.Tasks;
using Service.UserProgress.Grpc;
using Service.UserProgress.Grpc.Models;
using Service.UserProgress.Mapper;

namespace Service.UserProgress.Services
{
	public class UserProgressService : IUserProgressService
	{
		private readonly KnowledgeProgressService _knowledgeRepository;
		private readonly HabitProgressService _habitRepository;
		private readonly ISkillProgressService _skillProgressService;

		public UserProgressService(KnowledgeProgressService knowledgeRepository,
			HabitProgressService habitRepository, ISkillProgressService skillProgressService)
		{
			_knowledgeRepository = knowledgeRepository;
			_habitRepository = habitRepository;
			_skillProgressService = skillProgressService;
		}

		public async ValueTask<UnitedProgressGrpcResponse> GetUnitedProgressAsync(GetProgressGrpcRequset request) => new UnitedProgressGrpcResponse
		{
			Knowledge = await GetKnowledgeProgressAsync(request),
			Habit = await GetHabitProgressAsync(request),
			Skill = await GetSkillProgressAsync(request)
		};

		public async ValueTask<ProgressGrpcResponse> GetKnowledgeProgressAsync(GetProgressGrpcRequset request) =>
			(await _knowledgeRepository.GetData(request.UserId)).ToGrpcModel();

		public async ValueTask<AllProgressGrpcResponse> GetAllKnowledgeProgressAsync(GetAllProgressGrpcRequset request) =>
			(await _knowledgeRepository.GetDataAll(request.UserId)).ToGrpcModel();

		public async ValueTask<ProgressGrpcResponse> GetHabitProgressAsync(GetProgressGrpcRequset request) =>
			(await _habitRepository.GetData(request.UserId)).ToGrpcModel();

		public async ValueTask<AllProgressGrpcResponse> GetAllHabitProgressAsync(GetAllProgressGrpcRequset request) =>
			(await _habitRepository.GetDataAll(request.UserId)).ToGrpcModel();

		public async ValueTask<SkillProgressGrpcResponse> GetSkillProgressAsync(GetProgressGrpcRequset request) =>
			(await _skillProgressService.GetData(request.UserId)).ToGrpcModel();
	}
}