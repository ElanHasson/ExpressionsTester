using System;
using System.Linq.Expressions;

namespace ExpressionsTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var concatter = ConcatStringsExpression().Compile()(new object[] {"hello", " world"});
            Console.WriteLine(concatter);
        }

        public static ConstantExpression BuildConstant<T>(T constant)
        {
            return Expression.Constant(constant, typeof(T));
        }


        public static Expression<Func<object[], string>> ConcatStringsExpression()
        {
            var parameterParam = Expression.Parameter(typeof(object[]), "objectsToConcat");

            // String.Concat(object[])
            var stringConcat = typeof(string).GetMethod(nameof(string.Concat), new[] {typeof(object[])});
            var result = Expression.Call(stringConcat, parameterParam);

            return Expression.Lambda<Func<object[], string>>(result, parameterParam);
        }

        public static Expression BuildLambda(BlockExpression blockExpression)
        {
            return Expression.Lambda(blockExpression);
        }
    }
}