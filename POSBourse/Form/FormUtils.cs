using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSBourse.Bean;

namespace POSBourse.Form
{
    public static class FormUtils
    {
        public static List<ComboboxBean> GetReassortComoboboxItems()
        {
            List<ComboboxBean> ReassortComboboxItems = new List<ComboboxBean>();
            ReassortComboboxItems.Add(new ComboboxBean { Id = "REASSORTI", Value = "REASSORTI" });
            ReassortComboboxItems.Add(new ComboboxBean { Id = "NON REASSORTI", Value = "NON REASSORTI" });

            return ReassortComboboxItems;
        }
    }
}
