using ConsoleApp_Bank.Model;
using ConsoleApp_Bank.RulesEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Bank.Strategy
{
    /// <summary>
    /// Class that implements the strategy pattern, that contains the methods to evaluate trades againts the given set of rules.
    /// This may also be interpreted as a rule engine pattern.
    /// </summary>
    internal class TradeRiskCategoryStrategy
    {
        private readonly List<ITradeRiskCategoryRuleStrategy> _rules;

        /// <summary>
        /// Initializes a new instance of <see cref="TradeRiskCategoryStrategy"/> class.
        /// The constructor implements depency injection pattern to receive the rules that are to be used to evaluate the trade's risk.
        /// </summary>
        /// <param name="rules"></param>
        internal TradeRiskCategoryStrategy(List<ITradeRiskCategoryRuleStrategy> rules)
        {
            this._rules = rules;
        }
        /// <summary>
        /// Evaluate the trade category risk, based on the given rules.
        /// </summary>
        /// <param name="trade"></param>
        /// <returns>Category risk as string</returns>
        internal string TradeRiskCategorize(ITrade trade)
        {
            foreach (var rule in _rules)
            {
                if (rule.CanRun(trade))
                {
                    if (rule.TradeCategoryEvaluate(trade))
                        return rule.TradeCategoryRisk(trade);
                }

            }

            //ToDo: Review what would the proper behaviour be for trades that do not meet any criteria.
            return "UNCATECORIZED";
        }
        /// <summary>
        /// Evaluate a collection of trade's category risk, based on the given rules.
        /// </summary>
        /// <param name="trade"></param>
        /// <returns>Category risk as string</returns>
        internal IEnumerable<string> TradeRiskCategorize(List<ITrade> trade)
        {
            foreach (var t in trade)
            {
                yield return TradeRiskCategorize(t);
            }
            
        }
    }
}
