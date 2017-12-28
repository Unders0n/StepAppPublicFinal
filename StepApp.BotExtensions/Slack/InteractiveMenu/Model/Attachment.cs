using System.Collections.Generic;
using Newtonsoft.Json;

namespace StepApp.BotExtensions.Slack.InteractiveMenu.Model
{
    public class Attachment
    {

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("fallback")]
        public string Fallback { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("attachment_type")]
        public string AttachmentType { get; set; }

        [JsonProperty("callback_id")]
        public string CallbackId { get; set; }

        [JsonProperty("actions")]
        public IList<Action> Actions { get; set; }

        [JsonProperty("response_url")]
        public string ResponseUrl { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

    }
}