using System;
using System.Collections;

namespace StepApp.CommonExtensions.RandomExtensions
{
    public static class RandomExtensions
    {
        public static string GetRandomString(this IList collection)
        {
            var randomMsgIndex = new Random(Guid.NewGuid().GetHashCode()).Next(0, (collection.Count - 1));
            return collection[randomMsgIndex].ToString();
        }
    }
}
