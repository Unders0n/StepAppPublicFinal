using System;
using Microsoft.Bot.Builder.Dialogs;
using StepApp.CommonExtensions.String;

namespace StepApp.BotExtensions.HelpCommand
{
    public class HelpCommand
    {
        public static string GenerateText(Type classWithCommands)
        {
            var strReturn = "Commands available:" + "\n\n";
            foreach (var field in classWithCommands.GetFields())
            {
                if (field.GetCustomAttributes(typeof(HelpCommandDescriptionAttribute), true).Length > 0) {
                    HelpCommandDescriptionAttribute att = (HelpCommandDescriptionAttribute)field.GetCustomAttributes(typeof(HelpCommandDescriptionAttribute), true)[0];
                    if (att.ShowDescription)
                    {
                        strReturn += field.GetRawConstantValue() + " - " + att.Description + "\n\n";
                    }
                }
            }
            return strReturn;
        }

        public static string GenerateTextForLuisRootDialog(Type classWithCommands)
        {
            var strReturn = "Commands available:" + "\n\n";
            foreach (var field in classWithCommands.GetMethods())
            {
                if (field.GetCustomAttributes(typeof(HelpCommandDescriptionAttribute), true).Length == 0) continue;

                var text = "";
                if (field.GetCustomAttributes(typeof(LuisIntentAttribute), true).Length > 0)
                {
                    LuisIntentAttribute intentAtt = (LuisIntentAttribute)field.GetCustomAttributes(typeof(LuisIntentAttribute), true)[0];
                    text += "**" + intentAtt.IntentName.Substring(intentAtt.IntentName.IndexOf(".", StringComparison.Ordinal) + 1).CamelCaseToSpaces().ToLower() + "**";
                    
                }

                if (field.GetCustomAttributes(typeof(HelpCommandDescriptionAttribute), true).Length > 0)
                {
                    HelpCommandDescriptionAttribute att = (HelpCommandDescriptionAttribute)field.GetCustomAttributes(typeof(HelpCommandDescriptionAttribute), true)[0];
                    if (att.ShowDescription && att.Description.Length != 0)
                    {
                        text += " - " + att.Description;
                    }
                }
                strReturn += "\n\n" + text;
            }
            return strReturn;
        }
    }
}
