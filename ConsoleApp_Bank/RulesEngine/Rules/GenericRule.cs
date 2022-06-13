using ConsoleApp_Bank.Model;
using ConsoleApp_Bank.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Bank.RulesEngine.Rules
{

    /// <summary>
    /// Class thats represents a generic rule to determine if the risk category of a trade. <see cref="ITradeRiskCategoryRuleStrategy"/>
    /// </summary>
    public class GenericRule : ITradeRiskCategoryRuleStrategy
    {
        public string Name { get; }
        public List<GenericCondition> Conditions { get;}
        public string TypeName { get; }
        public string TypeNameSpace { get; }

        internal GenericRule(string name, List<GenericCondition> conditions)
        {
 
            Name = name;
            Conditions = conditions;
        }
      

        /// <summary>
        /// <see cref="ITradeRiskCategoryRuleStrategy.CanRun(ITrade)"/>
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public bool CanRun(ITrade trade)
        {
            return !string.IsNullOrEmpty(trade.ClientSector) && trade.Value > 0;
        }

        /// <summary>
        /// <see cref="ITradeRiskCategoryRuleStrategy.TradeCategoryEvaluate(ITrade)/>
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public bool TradeCategoryEvaluate(ITrade trade)
        {
            var expression = ExpressionFactory.GenericRuleExpressionBuilder<ITrade>(this);
            return expression(trade);

        }

        /// <summary>
        /// <see cref="ITradeRiskCategoryRuleStrategy.TradeCategoryRisk(ITrade)/>
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public string TradeCategoryRisk(ITrade trade)
        {
            return this.Name;
        }
    }
}
