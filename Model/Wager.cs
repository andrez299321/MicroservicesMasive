using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Roulette.EnumerationTypes;

namespace Roulette.Models
{
    public class Wager
    {
        public string Color { get; set; }
        public string Number { get; set; }
        public string Cash { get; set; }
        public string IdRoulette { get; set; }
    }
}
