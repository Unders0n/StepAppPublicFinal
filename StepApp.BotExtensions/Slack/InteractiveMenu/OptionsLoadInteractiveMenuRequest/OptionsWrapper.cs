using System.Collections.Generic;
using Newtonsoft.Json;
using StepApp.BotExtensions.Slack.InteractiveMenu.Model;

namespace StepApp.BotExtensions.Slack.InteractiveMenu.OptionsLoadInteractiveMenuRequest
{
    public class OptionsWrapper
    {
        [JsonProperty("options")]
        public List<Option> Options { get; set; }

        public OptionsWrapper(List<Option> options)
        {
            Options = options;
        }
    }
}
