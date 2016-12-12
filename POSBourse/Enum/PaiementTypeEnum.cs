using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSBourse.Enum
{
    public enum PaiementTypeEnum
    {
        CB,
        ESP,
        CHEQUE,
        AVOIR,
        PLUSIEURS,
        LES_DEUX
    }

    public static class PaiementTypeEnumMethods {
        
        public static PaiementTypeEnum fromValue(string value)
        {
            PaiementTypeEnum finalValue;

            System.Enum.TryParse(value, out finalValue);

            return finalValue;
        }
    }
}
