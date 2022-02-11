using System;
using System.Linq.Expressions;
using System.Reflection;
using Application.Extensions;
using FluentValidation.Internal;

namespace Application.Common.Helpers
{
    public class PropertyNameResolver
    {
        public static string CamelCasePropertyNameResolver(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            return DefaultPropertyNameResolver(type, memberInfo, expression).ToCamelCase();
        }

        #pragma warning disable IDE0060 // Remove unused parameter
        public static string DefaultPropertyNameResolver(Type type, MemberInfo memberInfo, LambdaExpression expression)
        #pragma warning restore IDE0060 // Remove unused parameter
        {
            if (expression != null)
            {
                var chain = PropertyChain.FromExpression(expression);
                if (chain.Count > 0)
                {
                    return chain.ToString();
                }
            }

            return memberInfo?.Name;
        }
    }
}
