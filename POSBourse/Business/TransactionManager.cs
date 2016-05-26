using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using POSBourse.Bean;

namespace POSBourse.Business
{
    public class TransactionManager
    {
        public decimal getRemisesSumFromCollection(ObservableCollection<TableRemise> RemiseCollection)
        {
            decimal valeur = 0;

            foreach (var remise in RemiseCollection)
            {
                decimal valeurDecimal = decimal.Parse(remise.Montant);
                valeur += valeurDecimal;
            }

            return valeur;
        }
    }
}
