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
using POSBourse.Popup;
using System.ComponentModel;
using System.Data.Entity;

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

        private Multipay multipayPopup;

        private decimal monnaiePayeeESP;
        private decimal monnaiePayeeCB;
        private decimal monnaiePayeeCHEQUE;

        private decimal aRendreAVOIR;
        private decimal aRendreESP;

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

            calculateOnUi(true, false, true, false, false, false);
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

            calculateOnUi(true, false, true, false, false, false);
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

            calculateOnUi(true, false, false, true, false, false);
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

            calculateOnUi(true, false, false, true, false, false);
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
                Montant = formattedValeur,
                Echange = "non",
                BonCadeau = CADCheckBox.IsChecked == true ? "oui" : "non",
                NC = NCCheckBox.IsChecked == true ? "oui" : "non"
            });

            NoAvoirBox.Text = "";
            AvoirCaisseBox.Text = "";
            AvoirValeurBox.Text = "";
            CADCheckBox.IsChecked = false;
            NCCheckBox.IsChecked = false;

            calculateOnUi(true, false, false, false, true, false);
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

            calculateOnUi(true, false, false, false, true, false);
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

            calculateOnUi(true, false, false, false, false, true);
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

            calculateOnUi(true, false, false, false, false, true);
        }

        private void calculateOnUi(Boolean refreshAPayerScreen, Boolean fromMultipay, Boolean updateProduct, Boolean updateEchangeDirect, Boolean updateAvoir, Boolean updateRemise)
        {
            //We reset all user entries before reculating if...
            if(updateProduct || updateEchangeDirect || updateAvoir || updateRemise)
            {
                resetUserMoneyEntries();
            }

            CalculResultBean calculBean = TransactionManager.getFinalCalculResultBean(RemiseCollection, AvoirCollection, EchangeDirectCollection, ProduitsCollection, this.monnaiePayeeESP, this.monnaiePayeeCB, this.monnaiePayeeCHEQUE, this.aRendreAVOIR, this.aRendreESP);

            MontantAvoirScreen.Text = calculBean.totalAvoir.ToString();
            EchangeDirectScreen.Text = calculBean.totalEchangeDirect.ToString();
            RemiseScreen.Text = calculBean.totalRemise.ToString();
            ARendreScreen.Text = calculBean.ARendre.ToString();

            if (calculBean.totalReductions > calculBean.totalProduits)
            {
                TypePaiementScreen.IsEnabled = false;
                ARendreCombobox.IsEnabled = true;
                ResteAPayerScreen.Text = "0";
                ARendreScreen.Text = "0";
                ARendreAvoirScreen.Text = (calculBean.totalReductions - calculBean.totalProduits).ToString();
            }
            else if(calculBean.totalReductions == calculBean.totalProduits)
            {
                ARendreCombobox.IsEnabled = false;
                TypePaiementScreen.IsEnabled = false;
                ARendreCombobox.IsEnabled = false;
                ARendreAvoirScreen.IsEnabled = false;
                ARendreEspScreen.IsEnabled = false;
                ARendreAvoirScreen.Text = "0";
                ARendreEspScreen.Text = "0";
                ResteAPayerScreen.Text = "0";
                ARendreScreen.Text = "0";
            }
            else
            {
                if (refreshAPayerScreen)
                {
                    ResteAPayerScreen.Text = calculBean.resteAPayer.ToString();
                }

                ARendreAvoirScreen.Text = "0";
                ARendreEspScreen.Text = "0";
                ARendreCombobox.IsEnabled = false;
                TypePaiementScreen.IsEnabled = true;
                ARendreAvoirScreen.IsEnabled = false;
                ARendreEspScreen.IsEnabled = false;
            }
            
            if (fromMultipay) {
                ResteAPayerScreen.Text = "0";
            }

            TotalAPayerScreen.Text = "A PAYER : " + calculBean.totalAPayer.ToString() + " €";
        }

        private void updateMonnaiePayee(object sender, KeyEventArgs e)
        {
            var monnaiePayeeStr = ResteAPayerScreen.Text.Replace(".", ",");

            string selectedItem = ((ComboboxBean)TypePaiementScreen.SelectedItem).Value;

            if (selectedItem == "ESP")
            {
                if (!decimal.TryParse(monnaiePayeeStr, out this.monnaiePayeeESP))
                {
                    this.monnaiePayeeESP = 0;
                }
            }
            if (selectedItem == "CB")
            {
                if (!decimal.TryParse(monnaiePayeeStr, out this.monnaiePayeeCB))
                {
                    this.monnaiePayeeCB = 0;
                }
            }
            if (selectedItem == "CHEQUE")
            {
                if (!decimal.TryParse(monnaiePayeeStr, out this.monnaiePayeeCHEQUE))
                {
                    this.monnaiePayeeCHEQUE = 0;
                }
            }

            calculateOnUi(false, false, false, false, false, false);
        }

        private void TypePaiementScreen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = ((ComboboxBean)TypePaiementScreen.SelectedItem).Value;

            if (selectedItem != "PLUSIEURS")
            {
                this.monnaiePayeeCB = 0;
                this.monnaiePayeeCHEQUE = 0;
                this.monnaiePayeeESP = 0;
            }

            CalculResultBean calculBean = TransactionManager.getFinalCalculResultBean(RemiseCollection, AvoirCollection, EchangeDirectCollection, ProduitsCollection, this.monnaiePayeeESP, this.monnaiePayeeCB, this.monnaiePayeeCHEQUE, this.aRendreAVOIR, this.aRendreESP);

            if (selectedItem == "PLUSIEURS")
            {
                multipayPopup = new Multipay();
                multipayPopup.Closing += TypePaiementScreen_Closing;
                multipayPopup.LoadData(calculBean);
                multipayPopup.ShowDialog();
            }
            else
            {
                MultipayText.Text = "";
            }

            if (selectedItem == "ESP") {
                ResteAPayerScreen.IsEnabled = true;
                calculateOnUi(true, false, false, false, false, false);
            }
            else if(selectedItem == "PLUSIEURS")
            {
                ResteAPayerScreen.IsEnabled = false;
                calculateOnUi(false, true, false, false, false, false);
            }
            else
            {
                ResteAPayerScreen.IsEnabled = false;
                calculateOnUi(true, false, false, false, false, false);
            }

        }

        void TypePaiementScreen_Closing(object sender, CancelEventArgs e)
        {
            this.monnaiePayeeESP = multipayPopup.result.ESP;
            this.monnaiePayeeCB = multipayPopup.result.CB;
            this.monnaiePayeeCHEQUE = multipayPopup.result.CHEQUE;

            calculateOnUi(true, true, false, false, false, false);

            MultipayText.Text = "ESP = " + multipayPopup.result.ESP.ToString() + "; CB = " + multipayPopup.result.CB.ToString() + "; CHEQUE = " + multipayPopup.result.CHEQUE.ToString();
        }

        private void ARendreCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.aRendreESP = 0;
            this.aRendreAVOIR = 0;

            string selectedItem = ((ComboboxBean)ARendreCombobox.SelectedItem).Value;

            CalculResultBean calculBean = TransactionManager.getFinalCalculResultBean(RemiseCollection, AvoirCollection, EchangeDirectCollection, ProduitsCollection, this.monnaiePayeeESP, this.monnaiePayeeCB, this.monnaiePayeeCHEQUE, this.aRendreAVOIR, this.aRendreESP);

            if (selectedItem == "AVOIR")
            {
                ARendreEspScreen.Text = "0";
                ARendreAvoirScreen.Text = calculBean.ARendreAvoir.ToString();
                ARendreAvoirScreen.IsEnabled = false;
                ARendreEspScreen.IsEnabled = false;
            }
            else if (selectedItem == "ESP")
            {
                ARendreAvoirScreen.Text = "0";
                ARendreEspScreen.Text = calculBean.ARendreESP.ToString();
                ARendreAvoirScreen.IsEnabled = false;
                ARendreEspScreen.IsEnabled = false;
            }
            else if (selectedItem == "LES DEUX")
            {
                ARendreAvoirScreen.IsEnabled = true;
                ARendreEspScreen.IsEnabled = true;
            }
        }

        private void ARendreAvoirScreen_KeyUp(object sender, KeyEventArgs e)
        {
            ARendreAvoirScreen.Text.Replace(".", ",");

            if (!decimal.TryParse(ARendreAvoirScreen.Text, out this.aRendreAVOIR))
            {
                return;
            }
            else
            {
                this.aRendreESP = 0;
                CalculResultBean calculBean = TransactionManager.getFinalCalculResultBean(RemiseCollection, AvoirCollection, EchangeDirectCollection, ProduitsCollection, this.monnaiePayeeESP, this.monnaiePayeeCB, this.monnaiePayeeCHEQUE, this.aRendreAVOIR, this.aRendreESP);
                ARendreEspScreen.Text = calculBean.ARendreESP.ToString();
            }
        }

        private void ARendreEspScreen_KeyUp(object sender, KeyEventArgs e)
        {
            ARendreEspScreen.Text.Replace(".", ",");

            if (!decimal.TryParse(ARendreEspScreen.Text, out this.aRendreESP))
            {
                return;
            }
            else
            {
                this.aRendreAVOIR = 0;
                CalculResultBean calculBean = TransactionManager.getFinalCalculResultBean(RemiseCollection, AvoirCollection, EchangeDirectCollection, ProduitsCollection, this.monnaiePayeeESP, this.monnaiePayeeCB, this.monnaiePayeeCHEQUE, this.aRendreAVOIR, this.aRendreESP);
                ARendreAvoirScreen.Text = calculBean.ARendreAvoir.ToString();
            }
        }

        private void resetUserMoneyEntries()
        {
            this.monnaiePayeeCB = 0;
            this.monnaiePayeeCHEQUE = 0;
            this.monnaiePayeeESP = 0;
            this.aRendreAVOIR = 0;
            this.aRendreESP = 0;
            MultipayText.Text = "";
        }

        private void ValiderTransactionButton_Click(object sender, RoutedEventArgs e)
        {
        
            using (var context = new bourseContainer())
            {
                CalculResultBean calculBean = TransactionManager.getFinalCalculResultBean(RemiseCollection, AvoirCollection, EchangeDirectCollection, ProduitsCollection, this.monnaiePayeeESP, this.monnaiePayeeCB, this.monnaiePayeeCHEQUE, this.aRendreAVOIR, this.aRendreESP);

                decimal giftCouponValue = calculBean.totalBonCadeau;
                decimal couponValue = calculBean.totalAvoir;
                decimal usedCouponExchangeValue = calculBean.totalEchangeAndAvoirUtil;
                decimal exchangeValue = calculBean.totalEchange;
                decimal convertedCouponExchangeValue = calculBean.avoirEchangeConverti;
                decimal discountOfferValue = calculBean.totalRemise;
                decimal payCardValue = calculBean.monnaiePayeeCB;
                decimal cashValue = calculBean.monnaiePayeeESP;
                decimal realCashValue = calculBean.monnaiePayeeReelESP;
                decimal emittedCouponValue = calculBean.ARendreAvoir;
                decimal emittedCashValue = calculBean.ARendreESP;
                decimal realEmittedCashValue = calculBean.ARendreReelESP;
                decimal totalSalesValue = calculBean.totalProduits;
                decimal totalBuyValue = calculBean.totalEchange;
                int productCount = calculBean.productCount;
                string store = ""; //TODO

                Transaction transaction = new Transaction
                {
                    giftCouponValue = giftCouponValue,
                    couponValue = couponValue,
                    usedCouponExchangeValue = usedCouponExchangeValue,
                    exchangeValue = exchangeValue,
                    convertedCouponExchangeValue = convertedCouponExchangeValue,
                    discountOfferValue = discountOfferValue,
                    paycardValue = payCardValue,
                    cashValue = cashValue,
                    realCashValue = realCashValue,
                    emittedCashValue = emittedCashValue,
                    realEmittedCashValue = realEmittedCashValue,
                    totalSalesValue = totalSalesValue,
                    totalBuyValue = totalBuyValue,
                    productCount = productCount,
                    store = store,
                    datetime = DateTime.Now
                };

                context.Entry(transaction).State = EntityState.Added;
                context.SaveChanges();

                foreach (var produit in this.ProduitsCollection)
                {
                    //update the product if it exists
                    if (!String.IsNullOrEmpty(produit.Code) && context.ProductSet.Any(p => p.code == produit.Code))
                    {

                        Product product = context.ProductSet.Single(p => p.code == produit.Code);

                        product.title = produit.Titre;
                        product.type = produit.Type;
                        product.editor = produit.Editeur;
                        product.author = produit.Auteur;

                        context.Entry(product).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                    //otherwise, add it
                    if (!String.IsNullOrEmpty(produit.Code) && !context.ProductSet.Any(p => p.code == produit.Code))
                    {
                        Product product = new Product
                        {
                            author = produit.Auteur,
                            code = produit.Code,
                            editor = produit.Editeur,
                            title = produit.Titre,
                            type = produit.Type
                        };

                        context.Entry(product).State = EntityState.Added;
                        context.SaveChanges();
                    }

                    decimal productPrice = 0;
                    decimal.TryParse(produit.Prix, out productPrice);

                    SoldProduct soldProduct = new SoldProduct
                    {
                        price = productPrice,
                        inStock = produit.Reassort == "REASSORTI" ? true : false,
                        datetime = DateTime.Now,
                        Transaction = transaction,
                        author = produit.Auteur,
                        code = produit.Code,
                        editor = produit.Editeur,
                        title = produit.Titre,
                        type = produit.Type
                    };

                    context.Entry(soldProduct).State = EntityState.Added;
                    context.SaveChanges();
                }

            }
        }
    }
}
