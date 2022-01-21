using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.UserProgress.Settings
{
	public class SettingsModel
	{
		[YamlProperty("UserProgress.SeqServiceUrl")]
		public string SeqServiceUrl { get; set; }

		[YamlProperty("UserProgress.ZipkinUrl")]
		public string ZipkinUrl { get; set; }

		[YamlProperty("UserProgress.ElkLogs")]
		public LogElkSettings ElkLogs { get; set; }

		[YamlProperty("UserProgress.ServerKeyValueServiceUrl")]
		public string ServerKeyValueServiceUrl { get; set; }

		[YamlProperty("UserProgress.ServiceBusReader")]
		public string ServiceBusReader { get; set; }

		[YamlProperty("UserProgress.ServiceBusWriter")]
		public string ServiceBusWriter { get; set; }

		[YamlProperty("UserProgress.KeyUserKnowledge")]
		public string KeyUserKnowledge { get; set; }

		[YamlProperty("UserProgress.KeyUserHabit")]
		public string KeyUserHabit { get; set; }

		[YamlProperty("UserProgress.KeyUserSkill")]
		public string KeyUserSkill { get; set; }
	}
}