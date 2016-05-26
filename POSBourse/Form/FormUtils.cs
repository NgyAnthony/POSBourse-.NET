using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSBourse.Bean;
using System.Collections.ObjectModel;

namespace POSBourse.Form
{
    public static class FormUtils
    {
        public static List<ComboboxBean> GetReassortComoboboxItems()
        {
            List<ComboboxBean> reassortComboboxItems = new List<ComboboxBean>();
            reassortComboboxItems.Add(new ComboboxBean { Id = "REASSORTI", Value = "REASSORTI" });
            reassortComboboxItems.Add(new ComboboxBean { Id = "NON REASSORTI", Value = "NON REASSORTI" });

            return reassortComboboxItems;
        }

        public static List<ComboboxBean> GetProduitsComoboboxItems()
        {
            List<ComboboxBean> produitsComboboxItems = new List<ComboboxBean>();
            produitsComboboxItems.Add(new ComboboxBean { Id = "LIVRE", Value = "LIVRE" });
            produitsComboboxItems.Add(new ComboboxBean { Id = "CD", Value = "CD" });
            produitsComboboxItems.Add(new ComboboxBean { Id = "VINYLE", Value = "VINYLE" });
            produitsComboboxItems.Add(new ComboboxBean { Id = "DVD", Value = "DVD" });
            produitsComboboxItems.Add(new ComboboxBean { Id = "BLU-RAY", Value = "BLU-RAY" });
            produitsComboboxItems.Add(new ComboboxBean { Id = "JEU", Value = "JEU" });
            produitsComboboxItems.Add(new ComboboxBean { Id = "AUTRE", Value = "AUTRE" });

            return produitsComboboxItems;
        }

        public static List<ComboboxBean> GetRemiseTypeComoboboxItems()
        {
            List<ComboboxBean> remiseTypeComboboxItems = new List<ComboboxBean>();
            remiseTypeComboboxItems.Add(new ComboboxBean { Id = "POURCENTAGE", Value = "POURCENTAGE" });
            remiseTypeComboboxItems.Add(new ComboboxBean { Id = "VALEUR", Value = "VALEUR" });

            return remiseTypeComboboxItems;
        }

    }
}
