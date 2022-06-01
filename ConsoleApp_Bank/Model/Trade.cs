using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Bank.Model
{
    /// <summary>
    /// Generic class of what represents a trade, <see cref="ITrade"/>.
    /// </summary>
    internal class Trade : ITrade
    {
        /// <summary>
        /// <see cref="ITrade.Value"/>
        /// </summary>
        public double Value { get; }
        /// <summary>
        /// <see cref="ITrade.ClientSector"/>
        /// </summary>
        public string ClientSector { get; }

        /// <summary>
        /// Initializes a new instance of a trade based on <see cref="ITrade"/> interface.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="clientSector"></param>
        /// <exception cref="ArgumentException"></exception>
        public Trade(double value, string clientSector)
        {
            if (clientSector != "Public" && clientSector != "Private")
                throw new ArgumentException("Client sector not recognized.");

            this.Value = value;
            this.ClientSector = clientSector;

        }
    }
}
