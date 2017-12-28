using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

//test 2
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
    }
}
