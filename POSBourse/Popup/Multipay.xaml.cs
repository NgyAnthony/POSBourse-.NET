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

        public static String VALEUR_A_ATTEINDRE_PREFIX = "Valeur à atteindre : ";

        public enum ERROR_CODES { CB_ERROR, ESP_ERROR, CHEQUE_ERROR, NONE };

        public Multipay()
        {
            result = new MultipayBean();
            InitializeComponent();
        }

        public void LoadData(CalculResultBean _calculBean)
        {
            this.calulBean = _calculBean;
            ValeurAAtteindre.Text = VALEUR_A_ATTEINDRE_PREFIX + _calculBean.resteAPayer;
        }

        private void refreshValeurAAtteindre()
        {
            this.result.RESTANT = this.calulBean.resteAPayer - (result.CB + result.CHEQUE + result.ESP);

            if(this.result.RESTANT < 0)
            {
                this.result.RESTANT = 0;
            }

            ValeurAAtteindre.Text = VALEUR_A_ATTEINDRE_PREFIX + this.result.RESTANT;

            if(this.result.RESTANT == 0)
            {
                MultipayValider.IsEnabled = true;
            }
            else
            {
                MultipayValider.IsEnabled = false;
            }
        }

        private ERROR_CODES refreshEntriesAndReturnError()
        {
            if (MultipayCB.Text.Length == 0)
            {
                result.CB = 0;
            }
            else if (!decimal.TryParse(MultipayCB.Text.ToString(), out result.CB))
            {
                return ERROR_CODES.CB_ERROR;
            }

            if (MultipayCHEQUE.Text.Length == 0)
            {
                result.CHEQUE = 0;
            }
            else if (!decimal.TryParse(MultipayCHEQUE.Text.ToString(), out result.CHEQUE))
            {
                return ERROR_CODES.CHEQUE_ERROR;
            }

            if (MultipayESP.Text.Length == 0)
            {
                result.ESP = 0;
            }
            else if (!decimal.TryParse(MultipayESP.Text.ToString(), out result.ESP))
            {
                return ERROR_CODES.ESP_ERROR;
            }

            return ERROR_CODES.NONE;
        }

        private void MultipayValider_Click(object sender, RoutedEventArgs e)
        {
            ERROR_CODES codes = refreshEntriesAndReturnError();
            switch(codes)
            {
                case ERROR_CODES.CB_ERROR:
                    MessageBox.Show("Le montant CB est mal formaté !");
                    break;
                case ERROR_CODES.CHEQUE_ERROR:
                    MessageBox.Show("Le montant CHEQUE est mal formaté !");
                    break;
                case ERROR_CODES.ESP_ERROR:
                    MessageBox.Show("Le montant ESP est mal formaté !");
                    break;
                case ERROR_CODES.NONE:
                    this.Close();
                    break;
            }
        }

        private void EspKeyUp(object sender, KeyEventArgs e)
        {
            refreshEntriesAndReturnError();
            refreshValeurAAtteindre();
        }

        private void CbKeyUp(object sender, KeyEventArgs e)
        {
            refreshEntriesAndReturnError();
            refreshValeurAAtteindre();
        }

        private void ChequeKeyUp(object sender, KeyEventArgs e)
        {
            refreshEntriesAndReturnError();
            refreshValeurAAtteindre();
        }
    }
}
