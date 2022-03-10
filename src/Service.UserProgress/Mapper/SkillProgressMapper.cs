using System;
using System.Linq;
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

			int CountProgress(int tasksCount, EducationTaskType taskType)
			{
				int allTasksCount = allTaskTypes.Count(type => type == taskType);

				return (int) Math.Round(tasksCount * 100 / (float) allTasksCount);
			}

			return new SkillProgressGrpcResponse
			{
				Concentration = CountProgress(dto.ConcentrationCount, EducationTaskType.Text),
				Perseverance = CountProgress(dto.PerseveranceCount, EducationTaskType.Video),
				Thoughtfulness = CountProgress(dto.ThoughtfulnessCount, EducationTaskType.Case),
				Memory = CountProgress(dto.MemoryCount, EducationTaskType.Test),
				Adaptability = CountProgress(dto.AdaptabilityCount, EducationTaskType.TrueFalse),
				Activity = CountProgress(dto.ActivityCount, EducationTaskType.Game),
				Total = (int) Math.Round(dto.TotalCount * 100 / (float) allTaskTypes.Length)
			};
		}
	}
}