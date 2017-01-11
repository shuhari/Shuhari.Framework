using System;
using System.Linq.Expressions;
using System.Reflection;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Linq
{
    /// <summary>
    /// Build linq expression
    /// </summary>
    public static class ExpressionBuilder
    {
        /// <summary>
        /// Given the selector is a property selector (that is, it is of format
        /// <code>x =&gt; x.Prop</code>, this method extract the target property member.
        /// </summary>
        /// <param name="selector">Property selector</param>
        /// <typeparam name="T">Class type</typeparam>
        /// <typeparam name="TProp">Property type</typeparam>
        /// <returns>Extracted property</returns>
        /// <exception cref="ArgumentException">Raise if <paramref name="selector"/>is not a property selector</exception>
        public static PropertyInfo GetProperty<T, TProp>(Expression<Func<T, TProp>> selector)
        {
            Expect.IsNotNull(selector, nameof(selector));
            PropertyInfo prop = null;

            var body = selector.Body;
            if (body is MemberExpression)
            {
                prop = ((MemberExpression)body).Member as PropertyInfo;
            }
            else if (body is UnaryExpression)
            {
                var unary = (UnaryExpression)body;
                if (unary != null && unary.Operand is MemberExpression)
                {
                    prop = ((MemberExpression)unary.Operand).Member as PropertyInfo;
                }
            }

            if (prop != null)
                return prop;
            else
                throw new ArgumentException(string.Format("{0} is not a valid property selector", selector));
        }

        /// <summary>
        /// Build a getter for property (x => x.Prop)
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static Delegate BuildGetter(PropertyInfo prop)
        {
            Expect.IsNotNull(prop, nameof(prop));

            var paramX = Expression.Parameter(prop.DeclaringType, "x");
            Expression body = Expression.Property(paramX, prop.Name);
            return Expression.Lambda(body, paramX).Compile();
        }

        /// <summary>
        /// Build setter for propery ((x, value) => x.Prop = value);
        /// </summary>
        /// <param name="prop">Property</param>
        /// <returns>Linq expression</returns>
        public static Delegate BuildSetter(PropertyInfo prop)
        {
            Expect.IsNotNull(prop, nameof(prop));

            var paramX = Expression.Parameter(prop.DeclaringType, "x");
            var paramValue = Expression.Parameter(prop.PropertyType, "value");
            var accessor = Expression.Property(paramX, prop);
            var body = Expression.Assign(accessor, paramValue);
            return Expression.Lambda(body, paramX, paramValue).Compile();
        }
    }
}
