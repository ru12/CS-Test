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
    /// Represents the functions that a trade category rules needs to implement
    /// </summary>
    public interface ITradeRiskCategoryRuleStrategy
    {
        /// <summary>
        /// Name of the rule that will be returned as the category
        /// </summary>
        string Name { get; }
        /// <summary>
        /// List of the conditions used to build the danamic criteria
        /// </summary>
        List<GenericCondition> Conditions { get;}

        /// <summary>
        /// Name of the object beeing used to be analysed
        /// </summary>
        string TypeName { get; }
        /// <summary>
        /// The name spaceo of the object beeing used to be analysed
        /// </summary>
        string TypeNameSpace { get; }


        /// <summary>
        /// Verify if the preconditions for the rule run is met.
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public bool CanRun(ITrade trade);

        /// <summary>
        /// Evaluates if the rule applies to the trade
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public bool TradeCategoryEvaluate(ITrade trade);

        /// <summary>
        /// Returns the trade risk string.
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public string TradeCategoryRisk(ITrade trade);

    }
}
