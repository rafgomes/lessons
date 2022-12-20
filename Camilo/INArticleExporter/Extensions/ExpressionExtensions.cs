using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Extensions
{
    public class ExpressionExtensions
    {
        public static void CheckForNullArgument(params Expression<Func<object>>[] expressions)
        {
            foreach( Expression<Func<object>> expression in expressions)
            {
                Func<object> function = expression.Compile();
                object argument = function();
                if (argument == null || (argument is string && string.IsNullOrEmpty(Convert.ToString(argument))) )
                {
                    var name = GetName(expression);
                    throw new ArgumentNullException(name);
                }
            }
        }

        public static string GetName(Expression<Func<object>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.MemberAccess) {
                return ((MemberExpression)expression.Body).Member.Name;
            }

            if (expression.Body.NodeType == ExpressionType.Convert && ((UnaryExpression)expression.Body).Operand.NodeType == ExpressionType.MemberAccess)
            {
                return ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member.Name;
            }
            throw new ArgumentNullException("Argument 'expression must like () => argumentName");
        }
    }
}
