using System.Collections.Generic;
using Newtonsoft.Json;

namespace StepApp.BotExtensions.Slack.InteractiveMenu.Model
{
    public class Action
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("options")]
        public IList<Option> Options { get; set; }

        [JsonProperty("data_source")]
        public string DataSource { get; set; }
        
        [JsonProperty("min_query_length")]
        public string MinQueryLength { get; set; }
    }
}