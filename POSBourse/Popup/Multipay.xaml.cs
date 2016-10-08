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
using System.Windows.Shapes;
using POSBourse.Bean;

namespace POSBourse.Popup
{
    /// <summary>
    /// Logique d'interaction pour Multipay.xaml
    /// </summary>
    public partial class Multipay : Window
    {
        public MultipayBean result { get; set; }

        public CalculResultBean calulBean { get; set; }

        public Multipay()
        {
            result = new MultipayBean();
            InitializeComponent();
        }

        public void LoadData(CalculResultBean _calculBean)
        {
            this.calulBean = _calculBean;

            ValeurAAtteindre.Text = "Valeur à atteindre :" + _calculBean.resteAPayer;
        }

        private void MultipayValider_Click(object sender, RoutedEventArgs e)
        {

            //Controls ...
            if (MultipayCB.Text.Length == 0)
            {
                result.CB = 0;
            }
            else if (!decimal.TryParse(MultipayCB.Text.ToString(), out result.CB))
            {
                MessageBox.Show("Le montant CB est mal formaté !");
                return;
            }

            if (MultipayCHEQUE.Text.Length == 0)
            {
                result.CHEQUE = 0;
            }
            else if (!decimal.TryParse(MultipayCHEQUE.Text.ToString(), out result.CHEQUE))
            {
                MessageBox.Show("Le montant CHEQUE est mal formaté !");
                return;
            }

            if (MultipayESP.Text.Length == 0)
            {
                result.ESP = 0;
            }
            else if (!decimal.TryParse(MultipayESP.Text.ToString(), out result.ESP))
            {
                MessageBox.Show("Le montant ESP est mal formaté !");
                return;
            }

            this.Close();
        
        }

    }
}
