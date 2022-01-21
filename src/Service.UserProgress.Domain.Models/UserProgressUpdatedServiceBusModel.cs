using System;
using System.Runtime.Serialization;

namespace Service.UserProgress.Domain.Models
{
	[DataContract]
	public class UserProgressUpdatedServiceBusModel
	{
		public const string TopicName = "myjeteducation-user-progress-updated";

		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		/// <summary>
		///     Кол-во заработанных привычек
		/// </summary>
		[DataMember(Order = 2)]
		public int HabitCount { get; set; }
	}
}