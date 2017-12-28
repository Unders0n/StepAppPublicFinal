using System.Collections.Generic;
using Newtonsoft.Json;

namespace StepApp.BotExtensions.Slack.InteractiveMenu.Model
{
    public class SlackMenuChannelData
    {

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("response_type")]
        public string ResponseType { get; set; }

        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; }
    }
}