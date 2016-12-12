using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSBourse.Bean;
using POSBourse.Enum;
using System.Collections.ObjectModel;

namespace POSBourse.Form
{
    public static class FormUtils
    {
        public static List<ComboboxBean> GetReassortComoboboxItems()
        {
            List<ComboboxBean> reassortComboboxItems = new List<ComboboxBean>();
            reassortComboboxItems.Add(new ComboboxBean { Id = ReassortTypeEnum.REASSORTI.ToString(), Value = "REASSORTI" });
            reassortComboboxItems.Add(new ComboboxBean { Id = ReassortTypeEnum.NON_REASSORTI.ToString(), Value = "NON REASSORTI" });

            return reassortComboboxItems;
        }

        public static List<ComboboxBean> GetProduitsComoboboxItems()
        {
            List<ComboboxBean> produitsComboboxItems = new List<ComboboxBean>();
            produitsComboboxItems.Add(new ComboboxBean { Id = ProductTypeEnum.LIVRE.ToString(), Value = "LIVRE" });
            produitsComboboxItems.Add(new ComboboxBean { Id = ProductTypeEnum.CD.ToString(), Value = "CD" });
            produitsComboboxItems.Add(new ComboboxBean { Id = ProductTypeEnum.VINYLE.ToString(), Value = "VINYLE" });
            produitsComboboxItems.Add(new ComboboxBean { Id = ProductTypeEnum.DVD.ToString(), Value = "DVD" });
            produitsComboboxItems.Add(new ComboboxBean { Id = ProductTypeEnum.BLURAY.ToString(), Value = "BLURAY" });
            produitsComboboxItems.Add(new ComboboxBean { Id = ProductTypeEnum.JEU.ToString(), Value = "JEU" });
            produitsComboboxItems.Add(new ComboboxBean { Id = ProductTypeEnum.AUTRE.ToString(), Value = "AUTRE" });

            return produitsComboboxItems;
        }

        public static List<ComboboxBean> GetRemiseTypeComoboboxItems()
        {
            List<ComboboxBean> remiseTypeComboboxItems = new List<ComboboxBean>();
            remiseTypeComboboxItems.Add(new ComboboxBean { Id = RemiseType.POURCENTAGE.ToString(), Value = "%" });
            remiseTypeComboboxItems.Add(new ComboboxBean { Id = RemiseType.VALEUR.ToString(), Value = "€" });

            return remiseTypeComboboxItems;
        }

        public static List<ComboboxBean> GetPaiementTypeComoboboxItems()
        {
            List<ComboboxBean> remiseTypeComboboxItems = new List<ComboboxBean>();
            remiseTypeComboboxItems.Add(new ComboboxBean { Id = PaiementTypeEnum.CB.ToString(), Value = "CB" });
            remiseTypeComboboxItems.Add(new ComboboxBean { Id = PaiementTypeEnum.ESP.ToString(), Value = "ESP" });
            remiseTypeComboboxItems.Add(new ComboboxBean { Id = PaiementTypeEnum.CHEQUE.ToString(), Value = "CHEQUE" });
            remiseTypeComboboxItems.Add(new ComboboxBean { Id = PaiementTypeEnum.PLUSIEURS.ToString(), Value = "PLUSIEURS" });

            return remiseTypeComboboxItems;
        }

        public static List<ComboboxBean> GetPaiementRendreComoboboxItems()
        {
            List<ComboboxBean> remiseTypeComboboxItems = new List<ComboboxBean>();
            remiseTypeComboboxItems.Add(new ComboboxBean { Id = PaiementTypeEnum.AVOIR.ToString(), Value = "AVOIR" });
            remiseTypeComboboxItems.Add(new ComboboxBean { Id = PaiementTypeEnum.ESP.ToString(), Value = "ESP" });
            remiseTypeComboboxItems.Add(new ComboboxBean { Id = PaiementTypeEnum.LES_DEUX.ToString(), Value = "LES DEUX" });

            return remiseTypeComboboxItems;
        }
    }
}
