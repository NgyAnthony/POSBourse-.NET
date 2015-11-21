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
using POSBourse.Entity;
using POSBourse.Bean;
using System.Windows.Threading;
using System.Text.RegularExpressions;

namespace POSBourse
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
                prix = prix,
                code = code,
                reassort = reassort,
                type = type,
                titre = titre,
                auteur = auteur,
                editeur = editeur
            };

            ProduitsDataGrid.Items.Add(tableProduct);

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

    }
}
