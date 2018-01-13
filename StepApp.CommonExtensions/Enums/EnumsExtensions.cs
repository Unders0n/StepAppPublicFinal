using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace StepApp.CommonExtensions.Enums
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription<TEnum>(this TEnum item)
            => item.GetType()
                   .GetField(item.ToString())
                   .GetCustomAttributes(typeof(DescriptionAttribute), false)
                   .Cast<DescriptionAttribute>()
                   .FirstOrDefault()?.Description ?? string.Empty;

        /// <summary>
        /// Get list of text descriptions (by DescriptionAttribute)
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="values">Enum values one by one</param>
        /// <returns></returns>
        public static List<string> GetEnumDescriptions<TEnum>(params TEnum[] values)
        {
            var lst = values.ToList();
            return EnumDescriptions(lst);
        }

        /// <summary>
        /// Get list of text descriptions (by DescriptionAttribute)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration">Enumeration type</param>
        /// <returns></returns>
        public static List<string> ToListDescriptions<T>(this Type enumeration)
        {
            var lst = Enum.GetValues(enumeration).Cast<T>().ToList();
            return EnumDescriptions(lst);
        }

        private static List<string> EnumDescriptions<TEnum>(List<TEnum> lst)
        {
            List<string> descriptionCollection = new List<string>();
            foreach (var choice in lst) descriptionCollection.Add(choice.GetEnumDescription());
            return descriptionCollection;
        }

        public static void SeedEnumValues<T, TEnum>(this IDbSet<T> dbSet, Func<TEnum, T> converter)
            where T : class => Enum.GetValues(typeof(TEnum))
                                   .Cast<object>()
                                   .Select(value => converter((TEnum)value))
                                   .ToList()
                                   .ForEach(instance => dbSet.AddOrUpdate(instance));

        public static List<T> ToList<T>(this Type enumeration)
        {
          return  Enum.GetValues(enumeration).Cast<T>().ToList();
        }
        
        /// <summary>
        /// Find enum value in given enum type by description string
        /// </summary>
        /// <typeparam name="TEnum">enum type</typeparam>
        /// <param name="str">String to find in corresponding Description</param>
        /// <returns></returns>
        public static TEnum FindEnumValueByDescriptionString<TEnum>(this string str)
        {
            return typeof(TEnum).ToList<TEnum>().FirstOrDefault(s => s.GetEnumDescription() == str);
        }
    }
}
