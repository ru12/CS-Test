using ConsoleApp_Bank.Strategy;
using System.Collections.Generic;

namespace ConsoleApp_Bank.RulesEngine.Rules
{
    internal interface ITradeRiskCategory
    {
        List<ITradeRiskCategoryRuleStrategy> CreateRules();
    }
}