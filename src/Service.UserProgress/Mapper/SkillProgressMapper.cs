using System.Collections.Generic;
using System.Linq;
using Service.Core.Client.Extensions;
using Service.Education.Helpers;
using Service.Education.Structure;
using Service.UserProgress.Grpc.Models;
using Service.UserProgress.Models;

namespace Service.UserProgress.Mapper
{
	public static class SkillProgressMapper
	{
		public static SkillProgressGrpcResponse ToGrpcModel(this SkillProgressDto dto)
		{
			EducationTaskType[] allTaskTypes = EducationHelper.GetProjections()
				.Select(tuple => EducationHelper.GetTask(tuple.Tutorial, tuple.Unit, tuple.Task).TaskType)
				.ToArray();

			int CountProgress(IEnumerable<int> tasksProgress, EducationTaskType? taskType = null)
			{
				int allTasksCount = allTaskTypes
					.WhereIf(taskType != null, type => type == taskType)
					.Count();

				return tasksProgress.Sum() / allTasksCount;
			}

			return new SkillProgressGrpcResponse
			{
				Concentration = CountProgress(dto.ConcentrationProgress, EducationTaskType.Text),
				Perseverance = CountProgress(dto.PerseveranceProgress, EducationTaskType.Video),
				Thoughtfulness = CountProgress(dto.ThoughtfulnessProgress, EducationTaskType.Case),
				Memory = CountProgress(dto.MemoryProgress, EducationTaskType.Test),
				Adaptability = CountProgress(dto.AdaptabilityProgress, EducationTaskType.TrueFalse),
				Activity = CountProgress(dto.ActivityProgress, EducationTaskType.Game),
				Total = CountProgress(dto.TotalProgress)
			};
		}
	}
}