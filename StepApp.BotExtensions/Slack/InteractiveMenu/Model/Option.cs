using Newtonsoft.Json;

namespace StepApp.BotExtensions.Slack.InteractiveMenu.Model
{
    public class Option
    {

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}