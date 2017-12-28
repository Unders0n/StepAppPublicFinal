using System;

namespace StepApp.BotExtensions.Slack.Etc
{
    public static class SlackExtensions
    {
        /// <summary>
        /// Get user id from user+team id in slack
        /// </summary>
        /// <param name="userIdWithTeam"></param>
        /// <returns></returns>
        public static string NormalizeSlackUserIdWithTeam(this string userIdWithTeam)
        {
            return userIdWithTeam.Substring(0, userIdWithTeam.IndexOf(":", StringComparison.Ordinal));
        }

        public static string FullBotWithTeamAndChannelIdToBotId(this string id)
        {
            return id.Substring(0, id.LastIndexOf(":", StringComparison.Ordinal));
        }
    }
}
