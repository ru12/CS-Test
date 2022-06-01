using ConsoleApp_Bank.Model;
using ConsoleApp_Bank.RulesEngine;
using ConsoleApp_Bank.RulesEngine.Rules;
using ConsoleApp_Bank.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConsoleApp_Bank.Tests")]
namespace ConsoleApp_Bank
{
    /// <summary>
    /// Console project to showcase the requirements of the technical test.
    /// This project will use the Factory, Strategy and Depedency Injecton patterns to solve the techinical test specifications.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            //Exemple protifolio of trades to be categorized.
            var portifolio = new List<ITrade>()
            {
                new GenericExempleTrade1(2000000,"Private"),
                new GenericExempleTrade2(400000,"Public"),
                new GenericExempleTrade3(500000,"Public"),
                new GenericExempleTrade4(3000000,"Public"),
            };


            var rules = new TradeRiskCategoryRulesFactory().CreateRules();
            var ruleEngine = new TradeRiskCategoryStrategy(rules);

            var tradeCategories = ruleEngine.TradeRiskCategorize(portifolio);
            foreach (var tradeRisk in tradeCategories)
            {
                Console.WriteLine(tradeRisk);
            }
        }
    }
}
