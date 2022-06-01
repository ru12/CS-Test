using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Bank.Model
{
    /// <summary>
    /// Represents the properties/fields that a trade must implement.
    /// A trade is a commercial negotiation between a bank and a client.
    /// </summary>
    public interface ITrade
    {
        /// <summary>
        /// Transaction amount in dollars
        /// </summary>
        double Value { get; }
        /// <summary>
        /// Identify the client's sector
        /// </summary>
        string ClientSector { get; }
    }
}
