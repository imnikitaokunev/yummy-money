﻿using Application.Common.Exceptions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Application.Extensions
{
    public static class OrderedQueryableExtensions
    {
        // https://stackoverflow.com/questions/41244/dynamic-linq-orderby-on-ienumerablet-iqueryablet?rq=1

        public static IOrderedQueryable<T> OrderBy<T>(
            this IQueryable<T> source,
            string property) =>
            ApplyOrder(source, property, "OrderBy");

        public static IOrderedQueryable<T> OrderByDescending<T>(
            this IQueryable<T> source,
            string property) =>
            ApplyOrder(source, property, "OrderByDescending");

        public static IOrderedQueryable<T> ThenBy<T>(
            this IOrderedQueryable<T> source,
            string property) =>
            ApplyOrder(source, property, "ThenBy");

        public static IOrderedQueryable<T> ThenByDescending<T>(
            this IOrderedQueryable<T> source,
            string property) =>
            ApplyOrder(source, property, "ThenByDescending");

        private static IOrderedQueryable<T> ApplyOrder<T>(
            IQueryable<T> source,
            string property,
            string methodName)
        {
            var props = property.Split('.');
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (var prop in props)
            {
                // Use reflection (not ComponentModel) to mirror LINQ.
                var pi = type.GetProperty(prop);

                if (pi == null)
                {
                    throw new PropertyNotFoundException(type.Name, property);
                }

                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            var result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                              && method.IsGenericMethodDefinition
                              && method.GetGenericArguments().Length == 2
                              && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}