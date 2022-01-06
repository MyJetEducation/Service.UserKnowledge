using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.UserKnowledge.Settings
{
    public class SettingsModel
    {
        [YamlProperty("UserKnowledge.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("UserKnowledge.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("UserKnowledge.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        [YamlProperty("UserKnowledge.ServerKeyValueServiceUrl")]
        public string ServerKeyValueServiceUrl { get; set; }

        [YamlProperty("UserKnowledge.KeyUserKnowledge")]
        public string KeyUserKnowledge { get; set; }
    }
}
