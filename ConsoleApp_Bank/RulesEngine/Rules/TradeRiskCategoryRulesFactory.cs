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
    internal class TradeRiskCategoryRulesFactory
    {
        /// <summary>
        /// Class that implements the factory pattern to create and maintain the collection of rules.
        /// Further implementations should extend the classs to retrieve parameters from a database and create dynamic rules based on a configurations/parameter database table or API.
        /// This factory class will implement hardcoded rules just as a demonstration of custom rule engile implementation.
        /// </summary>
        /// 

        private readonly Dictionary<Contextos, ITradeRiskCategory> _ruleStrategy;

        public TradeRiskCategoryRulesFactory()
        {
            _ruleStrategy = new Dictionary<Contextos, ITradeRiskCategory>();
            _ruleStrategy.Add(Contextos.Contabil, new TradeRiskCategoryContabil());
            _ruleStrategy.Add(Contextos.Offshore, new TradeRiskCategoryOffshore());
            _ruleStrategy.Add(Contextos.Onshore, new TradeRiskCategoryOnshore());

        }

        internal static List<ITradeRiskCategoryRuleStrategy> CreateDefaultRules()
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

            //MEDIUMRISK
            var conditionsMediumRisk = new List<GenericCondition>
                {
                    new GenericCondition
                    {

                         Operator = Operators.Equals,
                         PropertyName = "ClientSector",
                         Value = "Public"
                    },
                    new GenericCondition
                    {

                         Operator = Operators.GreaterThen,
                         PropertyName = "Value",
                         Value = "1000000"
                    },
                };

            rules.Add(new GenericRule("MEDIUMRISK", conditionsMediumRisk));


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

        internal List<ITradeRiskCategoryRuleStrategy> CreateRulesStragy(Contextos contexto)
        {
            if (_ruleStrategy.ContainsKey(contexto))
                return _ruleStrategy[contexto].CreateRules();

            return TradeRiskCategoryRulesFactory.CreateDefaultRules();

        }


    }
}
