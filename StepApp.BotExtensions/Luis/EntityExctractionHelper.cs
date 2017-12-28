using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Luis;

namespace StepApp.BotExtensions.Luis
{
    public static class EntityExctractionHelper
    {
        
        public static async Task<DateTime?> ExctractTimeOrDateTime(string timeStr, ILuisService luis)
        {
            //  var time = new DateTime?();
            //ask for working  hours 

            var luisResult = await luis.QueryAsync(timeStr, CancellationToken.None);
            if (luisResult != null)
                if (luisResult.Entities.Count != 0)
                {
                    var entity = luisResult.Entities[0];


                    if (entity.Type == "builtin.datetimeV2.datetime" || entity.Type == "builtin.datetimeV2.time")
                        foreach (var vals in entity.Resolution.Values)
                        {
                            var lst = vals as List<object>;
                            var dict = lst?.FirstOrDefault() as Dictionary<string, object>;


                            if (dict != null)
                            {
                                var time = DateTime.Parse(dict["value"].ToString());
                                return time;
                            }
                        }
                }
            return null;
        }
    }
}
