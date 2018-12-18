using System;
using System.Linq.Expressions;

namespace ExpressionsTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var stringConcat = ConcatStringsExpression();
            Console.WriteLine($"ConcatStrings: {stringConcat.Compile()(new object[] { "hello", " world" })}");


            // Passing a string
            var executeExpressionToUpper = BuildStaticMethodCallExpression<string>(typeof(ToUpper), nameof(ToUpper.Execute));
            Console.WriteLine($"{nameof(BuildStaticMethodCallExpression)}<string>(typeof(ToUpper), nameof(ToUpper.Execute)): {executeExpressionToUpper.Compile()("hello")}");

            ParameterExpression upperInputParam =
                Expression.Parameter(typeof(string), "p0");
             
            var expressionBlock = Expression.Block(Expression.Assign(upperInputParam, Expression.Constant("hello", typeof(string))), executeExpressionToUpper);
            var lambda = Expression.Lambda<Func<string>>(expressionBlock);

            Console.WriteLine(lambda.Compile()());
        }



        /// <summary>
        /// Builds an expression for a given type and method taking a single string parameter.
        /// </summary>
        /// <returns>An expression.</returns>
        public static Expression<Func<string, TReturnType>> BuildStaticMethodCallExpression<TReturnType>(Type type, string methodName)
        {
            var parameterParam = Expression.Parameter(typeof(string), "p0");

            var methodInfo = type.GetMethod(methodName, new[] { typeof(string) });
            var result = Expression.Call(methodInfo, parameterParam);

            return Expression.Lambda<Func<string, TReturnType>>(result, parameterParam);
        }

        /// <summary>
        /// Builds an expression for <see cref="String.Concat(object[])"/>
        /// </summary>
        /// <returns>An expression.</returns>
        public static Expression<Func<object[], string>> ConcatStringsExpression()
        {
            var parameterParam = Expression.Parameter(typeof(object[]), "objectsToConcat");

            var stringConcat = typeof(string).GetMethod(nameof(string.Concat), new[] {typeof(object[])});
            var result = Expression.Call(stringConcat, parameterParam);

            return Expression.Lambda<Func<object[], string>>(result, parameterParam);
        }
    }
}