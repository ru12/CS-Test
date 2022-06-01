using ConsoleApp_Bank.Model;
using ConsoleApp_Bank.RulesEngine.Rules;
using ConsoleApp_Bank.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConsoleApp_Bank.Tests
{
    /// <summary>
    /// Class for trade risk category rules testing.
    /// </summary>
    public class TradeRulesTest
    {
        [Fact(DisplayName = "Test 1 - Rules Factory")]
        [Trait("Functionality", "Must Create Rules")]
        public void MustCreateRules()
        {
            Assert.NotEmpty(new TradeRiskCategoryRulesFactory().CreateRules());
        }

        [Fact(DisplayName = "Test 2 - Technical test validation")]
        [Trait("Functionality", "Must assert with technical test premisse")]
        public void MustComplyWithInterviewAnswer()
        {
            var technicalTestTradeRiskCategories = new List<string>{ "HIGHRISK", "LOWRISK", "LOWRISK", "MEDIUMRISK" };

            var portifolio = new List<ITrade>()
            {
                new GenericExempleTrade1(2000000,"Private"),
                new GenericExempleTrade2(400000,"Public"),
                new GenericExempleTrade3(500000,"Public"),
                new GenericExempleTrade4(3000000,"Public"),
            };

            var rules = new TradeRiskCategoryRulesFactory().CreateRules();
            var ruleEngine = new TradeRiskCategoryStrategy(rules);

            //var tradeCategories = portifolio.Select(a => ruleEngine.TradeRiskCategorize(a))?.ToList();
            var tradeCategories = ruleEngine.TradeRiskCategorize(portifolio);
            Assert.True(technicalTestTradeRiskCategories.SequenceEqual(tradeCategories));
        }

        [Fact(DisplayName = "Test 3 - Categorize a single trade on random value")]
        [Trait("Functionality", "Must categorize risk on a ramdom value")]
        public void MustCategorizeRiskOnRandomValue()
        {
            var rules = new TradeRiskCategoryRulesFactory().CreateRules();
            var ruleEngine = new TradeRiskCategoryStrategy(rules);

            var rdn = new Random();
            var tradeCategories = ruleEngine.TradeRiskCategorize(new GenericExempleTrade1(rdn.Next(1000, 200000), "Private"));
            Assert.NotEmpty(tradeCategories);
        }

        [Fact(DisplayName = "Test 4 - Categorize on collection of random trades")]
        [Trait("Functionality", "Must not break on ramdom trades")]
        public void MustNotBreakOnRandomTrades()
        {
            var portifolio = new List<ITrade>();
            var rdn = new Random();
            for (int i = 0; i < 5; i++)
            {
                portifolio.Add(new GenericExempleTrade1(rdn.Next(0, 1000001), "Private"));
            };
            for (int i = 0; i < 5; i++)
            {
                portifolio.Add(new GenericExempleTrade1(rdn.Next(0, 1000001), "Public"));
            };

            var rules = new TradeRiskCategoryRulesFactory().CreateRules();
            var ruleEngine = new TradeRiskCategoryStrategy(rules);

            //var tradeCategories = portifolio.Select(a => ruleEngine.TradeRiskCategorize(a))?.ToList();
            var tradeCategories = ruleEngine.TradeRiskCategorize(portifolio);
            Assert.NotEmpty(tradeCategories);
        }

        [Fact(DisplayName = "Test 4 - HIGHRISK")]
        [Trait("Functionality", "Must categorize as HIGHRISK")]
        public void MustCategorizeAsHighRisk()
        {
            string clientSector = "Private";
            var rnd = new Random();
            double minimum = 0;
            double maximum = double.MaxValue;
            double testValue = rnd.NextDouble() * (maximum - minimum) + minimum;

            var rules = new TradeRiskCategoryRulesFactory().CreateRules();
            var ruleEngine = new TradeRiskCategoryStrategy(rules);

            var tradeCategory = ruleEngine.TradeRiskCategorize(new GenericExempleTrade1(testValue, clientSector));
            Assert.Equal("HIGHRISK", tradeCategory);
        }

        [Fact(DisplayName = "Test 5 - MEDIUMRISK")]
        [Trait("Functionality", "Must categorize as MEDIUMRISK")]
        public void MustCategorizeAsMediumRisk()
        {
            string clientSector = "Public";
            var rnd = new Random();
            double minimum = 0;
            double maximum = double.MaxValue;
            double testValue = rnd.NextDouble() * (maximum - minimum) + minimum;

            var rules = new TradeRiskCategoryRulesFactory().CreateRules();
            var ruleEngine = new TradeRiskCategoryStrategy(rules);

            var tradeCategory = ruleEngine.TradeRiskCategorize(new GenericExempleTrade1(3000000, clientSector));
            Assert.Equal("MEDIUMRISK", tradeCategory);
        }

        [Fact(DisplayName = "Test 6 - LOWRISK")]
        [Trait("Functionality", "Must categorize as LOWRISK")]
        public void MustCategorizeAsLowRisk()
        {
            string clientSector = "Public";
            var rnd = new Random();
            double minimum = 0;
            double maximum = 999999.99;
            double testValue = rnd.NextDouble() * (maximum - minimum) + minimum;
            
            var rules = new TradeRiskCategoryRulesFactory().CreateRules();
            var ruleEngine = new TradeRiskCategoryStrategy(rules);

            var tradeCategory = ruleEngine.TradeRiskCategorize(new GenericExempleTrade2(testValue, clientSector));
            Assert.Equal("LOWRISK", tradeCategory);
        }

    }
}
