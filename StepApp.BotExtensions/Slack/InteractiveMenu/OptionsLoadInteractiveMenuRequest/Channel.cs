using Newtonsoft.Json;

namespace StepApp.BotExtensions.Slack.InteractiveMenu.OptionsLoadInteractiveMenuRequest
{
    public class Channel
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}