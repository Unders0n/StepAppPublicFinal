using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StepApp.CommonExtensions.Expressions
{
    public static class ExpressionsExtensions
    {
        public static Dictionary<string, object> MethodToDictionary(Expression<Action> expression)
        {
            Dictionary<string, object> resultingDict = new Dictionary<string, object>();


            var funcExpression = (expression.Body as MethodCallExpression);

            if (funcExpression == null)
                throw new Exception("");

            string[] props = null;
            MethodCache.TryGetValue(funcExpression.Method.Name, out props);

            if (props == null || !props.Any())
            {
                props = funcExpression.Method.GetParameters().Select(x => x.Name).ToArray();
                MethodCache.Add(funcExpression.Method.Name, props);
            }


            for (int i = 0; i < props.Count(); i++)
            {
                var name = props[i];


                dynamic value;

                dynamic valueExpression = funcExpression.Arguments[0] as ConstantExpression;

                if (valueExpression != null)
                {
                    value = valueExpression.Value;
                }
                else
                {
                    // In this case you dont need EVEN name dictionary if you pass the parameters with same name  - var name = valueExpression.Member.Name;
                    valueExpression = funcExpression.Arguments[0] as MemberExpression;


                    //Handling passing Property instead of fill
                    dynamic info = valueExpression.Member as PropertyInfo;
                    //trick for compiler
                    info = info ?? valueExpression.Member as FieldInfo;

                    value = info.GetValue(((ConstantExpression)valueExpression.Expression).Value);
                }

                resultingDict.Add(name, value);
            }

            return resultingDict;

        }

        public static Dictionary<string, string[]> MethodCache = new Dictionary<string, string[]>();

    }
}
