using System.Linq;
using Newtonsoft.Json.Linq;

//using Microsoft.Bot.Builder.Dialogs;

namespace StepApp.CommonExtensions.Json
{
    public static class SerializationHelper
    {
        //TODO: refactor to one method (overload?)
        /// <summary>
        ///     Deserialising json string and unwrapping only part with needed node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="nameOfNode">Name of node to start from, "Data" by default</param>
        /// <returns></returns>
        public static T DeserializeAndUnwrapCollection<T>(string json, string nameOfNode = "Data") where T : class 
        {
            var jo = JObject.Parse(json);

            if (!jo.Properties().First(property => property.Name.ToLower() == nameOfNode.ToLower()).Value.Any())
                return null;
            return  (jo.Properties().First(property => property.Name.ToLower() == nameOfNode.ToLower()).Value.ToObject<T>());
        }

        public static T DeserializeAndUnwrapCollectionDeep<T>(string json, string nameOfNode = "Data") where T : class
        {
            var jo = JObject.Parse(json);
            return jo["related"]["items"].ToObject<T>();
           
        }

        /// <summary>
        ///     Deserialising json string and unwrapping only part with needed node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="nameOfNode">Name of node to start from, "Data" by default</param>
        /// <returns></returns>
        public static T DeserializeAndUnwrap<T>(string json, string nameOfNode = "Data") where T : class
        {
            var jo = JObject.Parse(json);
            if (!jo.Properties().First(property => property.Name.ToLower() == nameOfNode.ToLower()).HasValues)
                return null;
            return (jo.Properties().First(property => property.Name.ToLower() == nameOfNode.ToLower()).Value.ToObject<T>());
        }
    }
}