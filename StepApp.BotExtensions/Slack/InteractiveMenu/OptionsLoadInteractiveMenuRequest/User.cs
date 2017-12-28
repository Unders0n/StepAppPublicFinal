using Newtonsoft.Json;

namespace StepApp.BotExtensions.Slack.InteractiveMenu.OptionsLoadInteractiveMenuRequest
{
    public class User
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}