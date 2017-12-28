using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StepApp.CommonExtensions.Reflection
{
    public static class ReflectionExtensions
    {

        public static string ToStringShowingAllFields(this object obj)
        {
            StringBuilder sb = new StringBuilder();
            foreach (System.Reflection.FieldInfo property in obj.GetType().GetFields())
            {

                sb.Append(property.Name);
                sb.Append(": ");
                /* if (property..GetIndexParameters().Length > 0)
                 {
                     sb.Append("Indexed Property cannot be used");
                 }
                 else
                 {*/
                sb.Append(property.GetValue(obj));
                //}

                sb.Append(System.Environment.NewLine);
                sb.Append("\n");
            }

            return sb.ToString();
        }

        public static string ToStringShowingAllProperties(this object obj, string[] propertyNames)
        {
            StringBuilder sb = new StringBuilder();

            var props = obj.GetType().GetProperties().Where(info => propertyNames.Contains(info.Name));
            foreach (var propertyName in propertyNames)
            {
                var property = props.FirstOrDefault(info => info.Name == propertyName);
                if (property != null)
                {
                    sb.Append(property.Name);
                    sb.Append(": ");
                    if (property.GetIndexParameters().Length > 0)
                    {
                        sb.Append("Indexed Property cannot be used");
                    }
                    else
                    {
                        sb.Append(property.GetValue(obj));
                    }

                    sb.Append(System.Environment.NewLine);
                }
            }

            return sb.ToString();
        }

        public static Dictionary<string, string> ToDictionaryShowingProperties(this object obj, string[] propertyNames)
        {
            var dict = new Dictionary<string, string>();

            var props = obj.GetType().GetProperties().Where(info => propertyNames.Contains(info.Name));
            foreach (var propertyName in propertyNames)
            {
                var property = props.FirstOrDefault(info => info.Name == propertyName);
                if (property != null)
                {
                    if (property.GetValue(obj) != null)
                    {
                        dict.Add(property.Name, property.GetValue(obj).ToString());
                    }
                }
            }

            return dict;
        }
    }
}