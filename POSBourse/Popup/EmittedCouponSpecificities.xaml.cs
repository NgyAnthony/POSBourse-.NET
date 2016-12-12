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
using POSBourse.Enum;
using POSBourse.Entity;
using POSBourse.Business;

namespace POSBourse.Popup
{
    /// <summary>
    /// Logique d'interaction pour EmittedCouponSpecificities.xaml
    /// </summary>
    public partial class EmittedCouponSpecificities : Window
    {
        public TransactionRegister TransactionRegister { get; set; }
        public EmittedCouponSpecificityResultBean result { get; set; }
        public CalculResultBean calculBean { get; set; }
        public Transaction transaction { get; set; }

        public EmittedCouponSpecificities()
        {
            TransactionRegister = new TransactionRegister();
            result = new EmittedCouponSpecificityResultBean();
            InitializeComponent();
        }

        private void EmittedCouponValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if(EmittedCouponOnlyCDCheckbox.IsChecked == true)
            {
                result.onlyOn = ProductTypeEnum.CD;
            }

            if (EmittedCouponNCCheckbox.IsChecked == true)
            {
                result.transactionSpecificity = AvoirTypeEnum.NC;
            }

            //Registering Emitted Coupon...
            TransactionRegister.registerEmittedCouponForVente(calculBean, transaction, result.transactionSpecificity, result.onlyOn);

            EmittedCouponOnlyCDCheckbox.IsChecked = false;
            EmittedCouponNCCheckbox.IsChecked = false;

            this.Close();
        }
    }
}
