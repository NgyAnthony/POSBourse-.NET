using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using POSBourse.Entity;
using POSBourse.Bean;
using POSBourse.Form;
using POSBourse.Business;

namespace POSBourse
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TableProduct> ProduitsCollection { get; set; }
        public ObservableCollection<TableEchangeDirect> EchangeDirectCollection { get; set; }
        public ObservableCollection<TableAvoir> AvoirCollection { get; set; }
        public ObservableCollection<TableRemise> RemiseCollection { get; set; }
        public List<ComboboxBean> ReassortDataComboItems { get; set; }
        public List<ComboboxBean> ProduitsDataComboItems { get; set; }
        public List<ComboboxBean> RemiseTypeDataComboItems { get; set; }
        public List<ComboboxBean> PaiementTypesDataComboItems { get; set; }
        public List<ComboboxBean> PaiementRendreTypesDataComboItems { get; set; }
        public TransactionManager TransactionManager { get; set; }
        public decimal monnaiePayee;
       
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            ProduitsCollection = new ObservableCollection<TableProduct>();
            EchangeDirectCollection = new ObservableCollection<TableEchangeDirect>();
            AvoirCollection = new ObservableCollection<TableAvoir>();
            RemiseCollection = new ObservableCollection<TableRemise>();
            ReassortDataComboItems = FormUtils.GetReassortComoboboxItems();
            ProduitsDataComboItems = FormUtils.GetProduitsComoboboxItems();
            RemiseTypeDataComboItems = FormUtils.GetRemiseTypeComoboboxItems();
            PaiementTypesDataComboItems = FormUtils.GetPaiementTypeComoboboxItems();
            PaiementRendreTypesDataComboItems = FormUtils.GetPaiementRendreComoboboxItems();
            TransactionManager = new TransactionManager();
        }

        private void addProductIntoTable()
        {
            String prix = PrixTextBox.Text.Replace(".", ",");
            decimal prixDecimal;
            String code = CodeTextBox.Text;
            String reassort = ReassortSelectBox.Text;
            String type = TypeSelectBox.Text;
            String titre = TitreTextBox.Text;
            String auteur = AuteurTextBox.Text;
            String editeur = EditeurTextBox.Text;

            if (prix.Length == 0)
            {
                MessageBox.Show("Le prix est manquant !");
                return;
            }
            else if (!decimal.TryParse(prix, out prixDecimal))
            {
                MessageBox.Show("Le prix est mal formaté !");
                return;
            }
            else if(titre.Length == 0)
            {
                MessageBox.Show("Le titre est manquant !");
                return;
            }

            var formattedPrice = string.Format("{0:0.00}", prixDecimal);

            TableProduct tableProduct = new TableProduct
            {
                Prix = formattedPrice,
                Code = code,
                Reassort = reassort,
                Type = type,
                Titre = titre,
                Auteur = auteur,
                Editeur = editeur
            };

            this.ProduitsCollection.Add(tableProduct);

            PrixTextBox.Text = "";
            CodeTextBox.Text = "";
            TitreTextBox.Text = "";
            AuteurTextBox.Text = "";
            EditeurTextBox.Text = "";

            Dispatcher.BeginInvoke(
                        DispatcherPriority.ContextIdle,
                        new Action(delegate()
                        {
                            PrixTextBox.Focus();
                        }));

            calculateOnUi();
        }

        private void AjouterProduitButton_Click(object sender, RoutedEventArgs e)
        {
            addProductIntoTable();
        }

        private void EditeurTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Tab)
            {
                addProductIntoTable();
            }
        }

        private void Context_Delete_Produit(object sender, RoutedEventArgs e)
        {
            //Get the clicked MenuItem
            var menuItem = (MenuItem)sender;

            //Get the ContextMenu to which the menuItem belongs
            var contextMenu = (ContextMenu)menuItem.Parent;

            //Find the placementTarget
            var item = (DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                var toDeleteFromBindedList = (TableProduct)item.SelectedCells[0].Item;
                ProduitsCollection.Remove(toDeleteFromBindedList);
            }

            calculateOnUi();
        }

        private void ValidateEchangeDirect(object sender, RoutedEventArgs e)
        {
            string client = EchangeDirectNomClientBox.Text;
            string valeur = EchangeDirectValeurBox.Text.Replace(".", ","); 
            decimal valeurDecimal;

            if (client.Length == 0)
            {
                MessageBox.Show("Le client est manquant !");
                return;
            }
            else if (!decimal.TryParse(valeur, out valeurDecimal))
            {
                MessageBox.Show("La valeur est mal formatée !");
                return;
            }

            var formattedValeur = string.Format("{0:0.00}", valeurDecimal);

            EchangeDirectCollection.Add(new TableEchangeDirect
            {
                Client = client,
                Valeur = formattedValeur
            });

            EchangeDirectNomClientBox.Text = "";
            EchangeDirectValeurBox.Text = "";

            calculateOnUi();
        }

        private void Context_Delete_EchangeDirect(object sender, RoutedEventArgs e)
        {
            //Get the clicked MenuItem
            var menuItem = (MenuItem)sender;

            //Get the ContextMenu to which the menuItem belongs
            var contextMenu = (ContextMenu)menuItem.Parent;

            //Find the placementTarget
            var item = (DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                var toDeleteFromBindedList = (TableEchangeDirect)item.SelectedCells[0].Item;
                EchangeDirectCollection.Remove(toDeleteFromBindedList);
            }

            calculateOnUi();
        }

        private void ValidateAvoir(object sender, RoutedEventArgs e)
        {
            string noAvoir = NoAvoirBox.Text;
            string caisse = AvoirCaisseBox.Text;
            string valeur = AvoirValeurBox.Text.Replace(".", ",");
            decimal valeurDecimal;

            if (noAvoir.Length == 0)
            {
                MessageBox.Show("Le NO d'avoir est manquant !");
                return;
            }
            else if (!decimal.TryParse(valeur, out valeurDecimal))
            {
                MessageBox.Show("La valeur est mal formatée !");
                return;
            }

            var formattedValeur = string.Format("{0:0.00}", valeurDecimal);

            AvoirCollection.Add(new TableAvoir
            {
                NoAvoir = noAvoir,
                Caisse = caisse,
                Montant = formattedValeur
            });

            NoAvoirBox.Text = "";
            AvoirCaisseBox.Text = "";
            AvoirValeurBox.Text = "";

            calculateOnUi();
        }

        private void Context_Delete_Avoir(object sender, RoutedEventArgs e)
        {
            //Get the clicked MenuItem
            var menuItem = (MenuItem)sender;

            //Get the ContextMenu to which the menuItem belongs
            var contextMenu = (ContextMenu)menuItem.Parent;

            //Find the placementTarget
            var item = (DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                var toDeleteFromBindedList = (TableAvoir)item.SelectedCells[0].Item;
                AvoirCollection.Remove(toDeleteFromBindedList);
            }

            calculateOnUi();
        }

        private void ValidateRemise(object sender, RoutedEventArgs e)
        {
            string nomClient = RemiseNomClientBox.Text;
            string remiseType = ((ComboboxBean) RemiseTypeCombobox.SelectedItem).Id;
            string valeur = RemiseValeurBox.Text.Replace(".", ",");
            decimal valeurDecimal;

            if (!decimal.TryParse(valeur, out valeurDecimal))
            {
                MessageBox.Show("La valeur est mal formatée !");
                return;
            }

            var formattedValeur = string.Format("{0:0.00}", valeurDecimal);

            RemiseCollection.Add(new TableRemise
            {
                Client = nomClient,
                Type = remiseType,
                Valeur = valeur
            });

            RemiseNomClientBox.Text = "";
            RemiseValeurBox.Text = "";

            calculateOnUi();
        }

        private void Context_Delete_Remise(object sender, RoutedEventArgs e)
        {
            //Get the clicked MenuItem
            var menuItem = (MenuItem)sender;

            //Get the ContextMenu to which the menuItem belongs
            var contextMenu = (ContextMenu)menuItem.Parent;

            //Find the placementTarget
            var item = (DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                var toDeleteFromBindedList = (TableRemise)item.SelectedCells[0].Item;
                RemiseCollection.Remove(toDeleteFromBindedList);
            }

            calculateOnUi();

        }

        private void calculateOnUi()
        {
            CalculResultBean calculBean = TransactionManager.getFinalCalculResultBean(RemiseCollection, AvoirCollection, EchangeDirectCollection, ProduitsCollection, this.monnaiePayee);

            MontantAvoirScreen.Text = calculBean.totalAvoir.ToString();
            EchangeDirectScreen.Text = calculBean.totalEchangeDirect.ToString();
            RemiseScreen.Text = calculBean.totalRemise.ToString();
            ResteAPayerScreen.Text = calculBean.resteAPayer.ToString();
            ARendreScreen.Text = calculBean.ARendre.ToString();
            TotalAPayerScreen.Text = "A PAYER : " + calculBean.totalAPayer.ToString() + " €";
        }

        private void updateMonnaiePayee(object sender, KeyEventArgs e)
        {
            var monnaiePayeeStr = ResteAPayerScreen.Text;
            
            if (!decimal.TryParse(monnaiePayeeStr, out this.monnaiePayee))
            {
                this.monnaiePayee = 0;
                return;
            }

            calculateOnUi();
        }
    }
}
