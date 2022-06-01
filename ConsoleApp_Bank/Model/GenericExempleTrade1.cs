using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Bank.Model
{
    /// <summary>
    /// Generic class of what would be a trade of a specific type 1, <see cref="ITrade"/>
    /// </summary>
    internal class GenericExempleTrade1 : Trade
    {
        public GenericExempleTrade1(double value, string clientSector) : base(value, clientSector)
        {

        }
    }
}
