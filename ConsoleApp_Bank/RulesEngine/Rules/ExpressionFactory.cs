using ConsoleApp_Bank.Model;
using ConsoleApp_Bank.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Bank.RulesEngine.Rules
{
    /// <summary>
    /// Static class that generates a custom function to be used to analyse a objects's value based on criteria
    /// It currently only supports the AND expression type for the multiple criteria rules's condition, for the sake of simplicity. More expression types may be added.
    /// Opterators of type GreaterThan and LessThan currently converts values to double as a mean to enable number comparison.
    /// </summary>
    internal static  class ExpressionFactory
    {
        /// <summary>
        /// Returns the function to be used as a rule comparison.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="genericRule"></param>
        /// <returns></returns>
        internal static Func<T, bool> GenericRuleExpressionBuilder<T>(ITradeRiskCategoryRuleStrategy genericRule)
        {
            Expression expressionFinal = null;
            var parameterExpression = Expression.Parameter(Type.GetType($"{genericRule.TypeNameSpace}.{genericRule.TypeName}"), genericRule.TypeName);

            foreach (var c in genericRule.Conditions)
            {
                var property = Expression.Property(parameterExpression, c.PropertyName);

                BinaryExpression expression = null;

                switch (c.Operator)
                {
                    case Operators.GreaterThen:
                        expression = Expression.GreaterThan(property, Expression.Constant(Convert.ToDouble(c.Value)));
                        break;
                    case Operators.LessThen:
                        expression = Expression.LessThan(property, Expression.Constant(Convert.ToDouble(c.Value)));
                        break;
                    case Operators.Equals:
                        expression = Expression.Equal(property, Expression.Constant(c.Value)); ;
                        break;
                    default:
                        break;
                };
                expressionFinal = expressionFinal == null ? expressionFinal = expression : Expression.And(expressionFinal, expression);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(expressionFinal, parameterExpression);
            return lambda.Compile();
        }
    }
}
