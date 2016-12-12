using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSBourse.Enum;

namespace POSBourse.Bean
{
    public class EmittedCouponSpecificityResultBean
    {
        public ProductTypeEnum onlyOn { get; set; }

        public AvoirTypeEnum transactionSpecificity { get; set; }
    }
}
