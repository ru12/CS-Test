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
        internal List<ITradeRiskCategoryRuleStrategy> CreateRules()
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

            rules.Add(new GenericRule("HIGHRISK", conditionsHighRisk, "ITrade", "ConsoleApp_Bank.Model"));

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

            rules.Add(new GenericRule("MEDIUMRISK", conditionsMediumRisk, "ITrade", "ConsoleApp_Bank.Model"));


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

            rules.Add(new GenericRule("LOWRISK", conditionsLowRisk, "ITrade", "ConsoleApp_Bank.Model"));

            return rules;
        }

       
    }
}
