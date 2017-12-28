using Newtonsoft.Json;

namespace StepApp.BotExtensions.Slack.InteractiveMenu.OptionsLoadInteractiveMenuRequest
{
    public class OptionsLoadInteractiveMenuRequest
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("callback_id")]
        public string CallbackId { get; set; }

        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("channel")]
        public Channel Channel { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("action_ts")]
        public string ActionTs { get; set; }

        [JsonProperty("message_ts")]
        public string MessageTs { get; set; }

        [JsonProperty("attachment_id")]
        public string AttachmentId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}