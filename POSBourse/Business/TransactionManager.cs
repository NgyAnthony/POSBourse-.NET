using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using POSBourse.Bean;

namespace POSBourse.Business
{
    public class TransactionManager
    {
        public decimal getRemisesSumFromCollection(ObservableCollection<TableRemise> RemiseCollection)
        {
            decimal valeur = 0;

            foreach (var remise in RemiseCollection)
            {
                decimal valeurDecimal = decimal.Parse(remise.Montant);
                valeur += valeurDecimal;
            }

            return valeur;
        }

        public decimal getAvoirSumFromCollection(ObservableCollection<TableAvoir> AvoirCollection)
        {
            decimal valeur = 0;

            foreach (var avoir in AvoirCollection)
            {
                decimal valeurDecimal = decimal.Parse(avoir.Montant);
                valeur += valeurDecimal;
            }

            return valeur;
        }

        public decimal getEchangeDirectSumFromCollection(ObservableCollection<TableEchangeDirect> EchangeDirectCollection)
        {
            decimal valeur = 0;

            foreach (var echange in EchangeDirectCollection)
            {
                decimal valeurDecimal = decimal.Parse(echange.Valeur);
                valeur += valeurDecimal;
            }

            return valeur;
        }

        public decimal getProductsSumFromCollection(ObservableCollection<TableProduct> ProductsCollection)
        {
            decimal valeur = 0;

            foreach (var product in ProductsCollection)
            {
                decimal valeurDecimal = decimal.Parse(product.Prix);
                valeur += valeurDecimal;
            }

            return valeur;
        }

        public ObservableCollection<TableRemise> calculateMontantRemiseFromTableRemise(ObservableCollection<TableRemise> RemiseCollection,
            ObservableCollection<TableProduct> ProductsCollection,
            ObservableCollection<TableAvoir> AvoirCollection,
            ObservableCollection<TableEchangeDirect> EchangeDirectCollection)
        {
            CalculResultBean resultBean = getResultBeanWithoutRemises(AvoirCollection, EchangeDirectCollection, ProductsCollection);

            decimal totalAfterAvoirAndEchangeDirect = resultBean.totalProduits - (resultBean.totalAvoir + resultBean.totalEchangeDirect);
            
            foreach (var remise in RemiseCollection)
            {
                if(remise.Type == "POURCENTAGE")
                {
                    decimal valeurRemiseDecimal = decimal.Parse(remise.Valeur);

                    decimal calculatedRemiseValue = valeurRemiseDecimal * totalAfterAvoirAndEchangeDirect / 100;

                    totalAfterAvoirAndEchangeDirect -= calculatedRemiseValue;

                    remise.Montant = string.Format("{0:0.00}", totalAfterAvoirAndEchangeDirect);
                }
                else if(remise.Type == "VALEUR")
                {
                    remise.Montant = remise.Valeur;
                }
            }

            return RemiseCollection;
        }

        public CalculResultBean getResultBeanWithoutRemises(ObservableCollection<TableAvoir> AvoirCollection,
            ObservableCollection<TableEchangeDirect> EchangeDirectCollection,
            ObservableCollection<TableProduct> ProductsCollection)
        {
            decimal avoirsSum = getAvoirSumFromCollection(AvoirCollection);
            decimal echangeDirectSum = getEchangeDirectSumFromCollection(EchangeDirectCollection);
            decimal productsSum = getProductsSumFromCollection(ProductsCollection);
            
            CalculResultBean result = new CalculResultBean
            {
                totalProduits = productsSum,
                totalAvoir = avoirsSum,
                totalEchangeDirect = echangeDirectSum
            };
            
            return result;
        }

        public CalculResultBean getFinalCalculResultBean(ObservableCollection<TableRemise> RemiseCollection,
            ObservableCollection<TableAvoir> AvoirCollection,
            ObservableCollection<TableEchangeDirect> EchangeDirectCollection,
            ObservableCollection<TableProduct> ProductsCollection,
            
            decimal monnaiePayee)
        {
            CalculResultBean calculResultBean = getResultBeanWithoutRemises(AvoirCollection, EchangeDirectCollection, ProductsCollection);

            calculResultBean.monnaiePayee = monnaiePayee;

            //Calcul de la remise...
            ObservableCollection<TableRemise> CalculatedRemiseCollection = calculateMontantRemiseFromTableRemise(RemiseCollection, ProductsCollection, AvoirCollection, EchangeDirectCollection);
            calculResultBean.totalRemise = getRemisesSumFromCollection(CalculatedRemiseCollection);

            //Calcul total à payer...
            calculResultBean.totalAPayer = calculResultBean.totalProduits;

            //Calcul des réductions
            decimal reductions = (calculResultBean.totalAvoir + calculResultBean.totalRemise + calculResultBean.totalEchangeDirect);
            
            //Calcul reste à payer...
            //Si monnaie payée est renseigné on ne fait rien car il s'agit du même textField
            if(calculResultBean.monnaiePayee == 0)
            {
                calculResultBean.resteAPayer = calculResultBean.totalProduits - reductions;
                calculResultBean.resteAPayer = Math.Abs(calculResultBean.resteAPayer);
            }

            //Calcul monnaie à rendre...
            calculResultBean.ARendre = calculResultBean.monnaiePayee - calculResultBean.resteAPayer;
            calculResultBean.ARendre = Math.Abs(calculResultBean.ARendre);

            return calculResultBean;
        }
    }
}
