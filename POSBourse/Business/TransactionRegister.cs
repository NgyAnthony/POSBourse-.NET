using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSBourse.Entity;
using POSBourse.Bean;
using System.Data.Entity;
using System.Collections.ObjectModel;
using POSBourse.Enum;

namespace POSBourse.Business
{
    public class TransactionRegister
    {
        public Transaction registerTransaction(CalculResultBean calculBean, TransactionTypeEnum transactionType)
        {
            using (var context = new bourseContainer())
            {
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
                    datetime = DateTime.Now,
                    transactionType = transactionType.ToString()
                };

                context.Entry(transaction).State = EntityState.Added;
                context.SaveChanges();
                context.Entry(transaction).State = EntityState.Detached;

                return transaction;
            }
        }

        public void registerAvoirs(ObservableCollection<TableAvoir> AvoirCollection, Transaction transaction)
        {
            using (var context = new bourseContainer())
            {
                foreach (var avoir in AvoirCollection)
                {
                    decimal avoirValeur = 0;
                    decimal.TryParse(avoir.Montant, out avoirValeur);

                    string transactionSpecificity = avoir.NC == "oui" ? AvoirTypeEnum.NC.ToString() : null;
                    transactionSpecificity = avoir.BonCadeau == "oui" ? AvoirTypeEnum.BON_CADEAU.ToString() : null;
                    
                    EnteredCoupon enteredCoupon = new EnteredCoupon
                    {
                        transactionSpecificity = transactionSpecificity,
                        value = avoirValeur,
                        datetime = DateTime.Now,
                        store = avoir.Caisse,
                        exchange = avoir.Echange == "oui" ? true : false,
                        onlyOn = avoir.UniquementCD == "oui" ? ProductTypeEnum.CD.ToString() : null,
                        Transaction = transaction
                    };

                    context.Entry(enteredCoupon).State = EntityState.Added;
                    context.SaveChanges();
                    context.Entry(enteredCoupon).State = EntityState.Detached;
                }
            }
        }

        public void registerRemises(ObservableCollection<TableRemise> RemiseCollection, Transaction transaction)
        {
            using (var context = new bourseContainer())
            {
                foreach (var remise in RemiseCollection)
                {
                    decimal remiseValeur = 0;
                    decimal.TryParse(remise.Valeur, out remiseValeur);

                    EnteredDiscount discount = new EnteredDiscount
                    {
                        clientName = remise.Client,
                        datetime = DateTime.Now,
                        discountType = remise.Type,
                        discountValue = remiseValeur,
                        Transaction = transaction
                    };

                    context.Entry(discount).State = EntityState.Added;
                    context.SaveChanges();
                    context.Entry(discount).State = EntityState.Detached;
                }
            }
        }

        public void registerEchangeDirects(ObservableCollection<TableEchangeDirect> EchangeDirectCollection, Transaction transaction)
        {
            using (var context = new bourseContainer())
            {
                foreach (var echangeDirect in EchangeDirectCollection)
                {
                    decimal directExchangeValue = 0;
                    decimal.TryParse(echangeDirect.Valeur, out directExchangeValue);

                    EnteredDirectExchange enteredDirectExchange = new EnteredDirectExchange
                    {
                        clientName = echangeDirect.Client,
                        datetime = DateTime.Now,
                        Transaction = transaction
                    };

                    context.Entry(enteredDirectExchange).State = EntityState.Added;
                    context.SaveChanges();
                    context.Entry(enteredDirectExchange).State = EntityState.Detached;
                }

            }
        }

        public void registerEmittedCouponForVente(CalculResultBean calculBean, Transaction transaction, AvoirTypeEnum transactionSpecificity, ProductTypeEnum onlyOn)
        {
            using (var context = new bourseContainer())
            {
                EmittedCoupon emittedCoupon = new EmittedCoupon
                {
                    datetime = DateTime.Now,
                    value = calculBean.ARendreAvoir,
                    Transaction = transaction,
                    onlyOn = onlyOn.ToString(),
                    transactionSpecificity = transactionSpecificity.ToString()
                };
                
                context.Entry(emittedCoupon).State = EntityState.Added;
                context.SaveChanges();
                context.Entry(emittedCoupon).State = EntityState.Detached;
            }
        }

        public void registerProduits(ObservableCollection<TableProduct> ProduitsCollection, Transaction transaction)
        {
            using (var context = new bourseContainer())
            {
                foreach (var produit in ProduitsCollection)
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
                        context.Entry(product).State = EntityState.Detached;
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
                        context.Entry(product).State = EntityState.Detached;
                    }

                    decimal productPrice = 0;
                    decimal.TryParse(produit.Prix, out productPrice);

                    SoldProduct soldProduct = new SoldProduct
                    {
                        price = productPrice,
                        inStock = produit.Reassort == ReassortTypeEnum.REASSORTI.ToString() ? true : false,
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
                    context.Entry(soldProduct).State = EntityState.Detached;
                }

            }
        }
        
    }
}
