using System.Runtime.CompilerServices;
using StepApp.CommonExtensions.String;

namespace StepApp.BotExtensions.Luis
{
    public static class LuisHelper
    {
        /// <summary>
        /// Returning text about recognised operation. Reflection is used to take method name.
        /// </summary>
        /// <param name="caller"></param>
        /// <returns></returns>
        public static string PostPerformingOperationNameMessage([CallerMemberName] string caller = "")
        {
            var stringWithSpaces = caller.CamelCaseToSpaces();
            return $"performing **{stringWithSpaces}** command...";
        }
    }
}
