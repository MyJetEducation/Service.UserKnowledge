using System;
using System.Threading.Tasks;
using Service.Core.Domain.Models.Education;

namespace Service.UserProgress.Domain.Models
{
	public interface IDtoRepository
	{
		ValueTask<ProgressDto> GetData(Guid? userId);

		ValueTask<ProgressDto[]> GetDataAll(Guid? userId);

		ValueTask SetData(Guid? userId, EducationTutorial tutorial, int unit, int task);
	}
}