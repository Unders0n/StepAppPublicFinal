using Newtonsoft.Json;

namespace StepApp.BotExtensions.Slack.InteractiveMenu.OptionsLoadInteractiveMenuRequest
{
    public class Team
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }
    }
}
