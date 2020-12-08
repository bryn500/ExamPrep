using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Reflection
{
    public class LambdaCreation
    {
        public void DoStuff()
        {
            var expressions = new List<Expression<Func<TestData, bool>>>();
            foreach (var lookup in Enumerable.Range(0, 2))
            {
                // manually build lambda from ColumnName property in dictionary
                var x = Expression.Parameter(typeof(TestData), "x");
                var body = Expression.PropertyOrField(x, "MyProp");
                var lambda = Expression.Lambda<Func<TestData, bool>>(body, x);

                // add to list to use in where 
                expressions.Add(lambda);
            }

            // combine expressions using 'Or' instead of 'And'
            IQueryable<TestData> numbers = Enumerable.Range(0, 10).Select(x => new TestData() { MyProp = x }).AsQueryable();
            var checker = numbers.Where(expressions.AnyOf());

            Console.WriteLine("");
            foreach (var result in checker)
                Console.Write(result + ",");
            Console.WriteLine("");
        }
    }

    public class TestData
    {
        public int MyProp { get; set; }
    }

    public static class Extensions
    {
        /// <summary>
        /// Flattens a list of boolean expressions into one by combining them each with OrElse 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> AnyOf<T>(this IList<Expression<Func<T, bool>>> expressions)
        {
            if (expressions == null || !expressions.Any())
                throw new ArgumentException("No expressions sent to AnyOf");

            // if just one expression return it as it was
            if (expressions.Count == 1)
                return expressions[0];

            // create starting expression from first in list
            var body = expressions[0].Body;
            var param = expressions[0].Parameters.Single();

            // loop over remaining expressions
            foreach (var expr in expressions.Skip(1))
            {
                // rewrite the body of this expression using first expression's parameter
                var swappedParam = new SwapVisitor(expr.Parameters.Single(), param)
                                    .Visit(expr.Body);

                // append the expression as an OrElse
                body = Expression.OrElse(body, swappedParam);
            }

            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }    

    public class SwapVisitor : ExpressionVisitor
    {
        private readonly Expression from, to;

        public SwapVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }

        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }
}
