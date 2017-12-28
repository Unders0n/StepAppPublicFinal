using System;
using System.Web;

namespace StepApp.CommonExtensions.Assembly
{
    public static class AssemblyHelper
    {
        /// <summary>
        /// Get assembly version to get exact version of running application. Can be used in external projects, returning version of CALLING app
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetCallingAssembly()
                                          .GetName()
                                          .Version
                                          .ToString();
        }

        public static DateTime GetAssemblyVersionDate()
        {
            var version = System.Reflection.Assembly.GetCallingAssembly()
                .GetName()
                .Version;
            var build = version.Build;

            var revision = version.Revision;

            var result = new DateTime(2000, 1, 1);
            result = result.AddDays(build);
            result = result.AddSeconds(revision * 2);
            return result;


        }
        /// <summary>
        /// Returns host uri 
        /// </summary>
        /// <returns></returns>
        public static string GetAppHostUri()
        {
            Uri hostUri = HttpContext.Current.Request.Url;
            return hostUri.ToString();
        }
    }
}
