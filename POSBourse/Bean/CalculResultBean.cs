using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSBourse.Bean
{
    public class CalculResultBean
    {
        public decimal totalAPayer { get; set; }
        public decimal resteAPayer { get; set; }
        public decimal ARendre { get; set; }
        public decimal monnaiePayee { get; set; }
        public decimal totalRemise { get; set; }
        public decimal totalAvoir { get; set; }
        public decimal totalEchangeDirect { get; set; }
        public decimal totalReductions { get; set; }
        public decimal totalProduits { get; set; }
        public decimal ARendreAvoir { get; set; }
        public decimal ARendreESP { get; set; }

    }
}
