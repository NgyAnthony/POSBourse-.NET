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

namespace POSBourse
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TableProduct> ProduitsCollection { get; set; }
        public List<ComboboxBean> ReassortDataComboItems { get; set; }
        public List<ComboboxBean> ProduitsDataComboItems { get; set; }
       
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            ProduitsCollection = new ObservableCollection<TableProduct>();
            ReassortDataComboItems = FormUtils.GetReassortComoboboxItems();
            ProduitsDataComboItems = FormUtils.GetProduitsComoboboxItems();
        }

        private void addProductIntoTable()
        {
            String prix = PrixTextBox.Text;
            prix = prix.Replace(".", ",");
            String code = CodeTextBox.Text;
            String reassort = ReassortSelectBox.Text;
            String type = TypeSelectBox.Text;
            String titre = TitreTextBox.Text;
            String auteur = AuteurTextBox.Text;
            String editeur = EditeurTextBox.Text;

            decimal num;

            if (prix.Length == 0)
            {
                MessageBox.Show("Le prix est manquant !");
                return;
            }
            else if (!decimal.TryParse(prix, out num))
            {
                MessageBox.Show("Le prix est mal formaté !");
                return;
            }
            else if(titre.Length == 0)
            {
                MessageBox.Show("Le titre est manquant !");
                return;
            }

            TableProduct tableProduct = new TableProduct
            {
                Prix = prix,
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

            //Get the underlying item, that you cast to your object that is bound
            //to the DataGrid (and has subject and state as property)
            var toDeleteFromBindedList = (TableProduct)item.SelectedCells[0].Item;

            //Remove item into observable collection.
            ProduitsCollection.Remove(toDeleteFromBindedList);
        }

    }
}
