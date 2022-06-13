using ConsoleApp_Bank.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Bank.RulesEngine.Rules
{
    internal class TradeRiskCategoryOnshore : ITradeRiskCategory
    {
        public List<ITradeRiskCategoryRuleStrategy> CreateRules()
        {
            var rules = new List<ITradeRiskCategoryRuleStrategy>();


            //HIGHRISK
            var conditionsHighRisk = new List<GenericCondition>
                {
                    new GenericCondition
                    {

                         Operator = Operators.Equals,
                         PropertyName = "ClientSector",
                         Value = "Private"
                    },
                    new GenericCondition
                    {

                         Operator = Operators.GreaterThen,
                         PropertyName = "Value",
                         Value = "1000000"
                    },
                };

            rules.Add(new GenericRule("HIGHRISK", conditionsHighRisk));


            //LOWRISK
            var conditionsLowRisk = new List<GenericCondition>
                {
                    new GenericCondition
                    {

                         Operator = Operators.Equals,
                         PropertyName = "ClientSector",
                         Value = "Public"
                    },
                    new GenericCondition
                    {

                         Operator = Operators.LessThen,
                         PropertyName = "Value",
                         Value = "1000000"
                    },
                };

            rules.Add(new GenericRule("LOWRISK", conditionsLowRisk));

            return rules;
        }
    }
}
