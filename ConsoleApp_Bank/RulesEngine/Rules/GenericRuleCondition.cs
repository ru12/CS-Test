using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Bank.RulesEngine.Rules
{
    public enum Operators
    {
        GreaterThen,
        LessThen,
        Equals
    }
    /// <summary>
    /// Class that defines a condition to be used on the custom expression builder.
    /// It currently uses the enum Operatar with 3 main operations, for the sake of simplicity. Further operators may be implemented as needed.
    /// </summary>
    public class GenericCondition
    {
        /// <summary>
        /// Operator of the condition.
        /// </summary>
        public Operators Operator { get; set; }
        /// <summary>
        /// Value that needs to be compared.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// The name of the property of a given object that will be analysed.
        /// </summary>
        public string PropertyName { get; set; }

    }
}
